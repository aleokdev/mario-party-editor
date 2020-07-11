## INTRODUCTION
I wanted to create a simple editor I could use to change texts in Mario Party DS without messing around
with 5000 different files, editors, custom scripts and decompression tools.
Instead of directly modifying the ROM like other editors, this one generates patch files for every single
file change, making single-file revertions and source control much, much easier to accomplish. This also
means you won't need a trillion different NDS files or full patches to distinguish your ROM versions.

## FAQ
Q: _Does this work on other NDS games apart from Mario Party DS?_

A: Yes, absolutely, although some features, such as the Text Table Editor, are specialized for
Mario Party.

Q: _Will you add support for other NDS game fileformats?_

A: Probably not, but if I do, it'll be for the game _Hotel Dusk: Room 215_.

Q: _Why use this editor instead of other ones, such as Tinke, NSMBe, NDSTool, CrystalTile,
kiwi.ds...?_

A: To be honest, I didn't create this project to replace any other editors available because I knew
I was going to create another one to add to the pile. This one is just specialized for my needs.

Real response: Look at the introduction. _Instead of directly modifying the ROM like other editors,
this one generates patch files for every single file change, making single-file revertions and source
control much, much easier to accomplish. This also means you won't need a trillion different NDS
files or full patches to distinguish your ROM versions._

## TECHNICAL STUFF
In this section I'll be documenting every single step I have took in order to create proper NDS files
and how I have solved all the problems I have encountered until now, since I have mediocre memory and
can't remember what I've done two days ago.

### GOTCHAS (Important, read this first!)
Quick TL;DR: We can't change any of the headers, but we can modify the file tables without moving or resizing them.

The wireless function uses checksums for the arm9, arm7 and general headers. These are encrypted together
with SHA-1 to create a key that is checked with nintendo's public key. Decyphering this key is almost
impossible so changing the headers is out of the question.

HOWEVER, it is still possible to change stuff like the filetable without modifying the header, as long as
we don't change the size of the total filetable or nametable. This is because the FAT/FNT is not stored
inside of the header, but the size and address of it are.

More specifically:

   Object       |   Address   |  Size
----------------|-------------|-------------
File Name Table | Header 0x40 | Header 0x44
FAT             | Header 0x48 | Header 0x4C

[Source](https://github.com/Roughsketch/mdnds/wiki/NDS-Format)

For simplicity, let's assume these values are constant, since we can't change them anyways. This means that Mario Party DS
has these constant values:

   Object       |   Address   |  Size
----------------|-------------|--------------------------------
File Name Table | 0x00308800  | 0x00003479 Bytes (13433 Bytes)
FAT             | 0x0030BE00  | 0x00001610 Bytes (5648 Bytes)

We are free to change the values contained in these partitions as much as we want.

### THE FILESYSTEM
First, some source links: The [header](http://svn.blea.ch/thdslib/trunk/thdslib/source/arm9/include/nitrofs.h) and [the source file](http://svn.blea.ch/thdslib/trunk/thdslib/source/arm9/source/nitrofs.c).

NDS files use a filesystem called Nitro to store and keep track of their data. It is composed of a **File Name Table** or
**FNT** for short that contains the filesystem structure and names, and a **File Allocation Table** or **FAT** that indicates
where each file is stored in the ROM.

FNT Format:
```protobuf
// This struct represents the metadata for the directory entry with ID
// (0xF000 + (offset_from_fnt / sizeof(FNTDirectoryEntry)), where sizeof(FNTDirectoryEntry) == 8.
// It points to many chained file/directory FNTNamedEntries that are inside of it. The chain ends with a null byte.
struct FNTDirectoryEntry {
    // The FNTNamedEntry address that this directory entry points to, relative to the FNT start.
    entry_relative_address : uint32,
    // The entry ID of the first file present in this folder. The second file has this ID + 1, the third has
    // this ID + 2, etc. This does not affect subdirectories.
    first_file_id : uint16,
    // The entry ID of the parent folder
    // If the parent is the filesystem root, this value doesn't start with 0xF000, and might indicate number of
    // files?
    parent_dir_id : uint16
};
struct FNTNamedEntry {
    is_directory : bool (1 bit),
    name_length : uint8 (7 bits),
    name : char8[name_length] // Encoding: UTF-8/ASCII
    if is_directory => entry_id : uint16 // Directories always start with 0xF000 in their ID
    // This struct will repeat for every single file/directory there is available in the directory.
    // This chain will stop when is_directory + name_length is 0. (Basically, it ends in a null byte.)
};

// As far as I know, there is no integer present in the metadata containing the number of directory entries.
// So to figure out how many directory entries there are, keep track of the first entry relative address and read
// up to that point.
directories : FNTDirectoryEntry[]
```
To look up the editor implementation of the filesystem, look at the NDS*.cs files inside the ROM folder in the
main project (More specifically, NDSFilesystem.cs). I tried to make the implementation as simple and easy to
understand as possible.

The FAT's format is much simpler:
```protobuf
struct FATEntry {
    // Those are addresses relative to the start of the ROM that mark where an entry's data starts and ends.
    lower_bound : uint32,
    upper_bound : uint32
}
// Only way that I know of to get the number of FAT entries is to count the number of files in the FNT.
entries : FATEntry[]
```
Each file's FNT entry corresponds to its FAT one, so if for example you want to obtain the contents of
the file `0x02` you'd access `entries[0x02]`, get the lower and upper bounds and map those to the ROM.

### LZ77 COMPRESSION
Most files in the Mario Party DS ROM and most Nintendo-developed DS games use a custom implementation
of LZ77 compression to optimize the amount of data used in files.

Normally, the compressed files end their filename with "_LZ.bin", so it's not hard to identify them.

The compressed files in Mario Party DS have the following format:
```protobuf
// This first byte, as far as I know, is completely unused, and it's not consistent between different
// files.
unknown : byte,
// The uncompressed file size. Can be used for checking if the decompression went right. And yes, it
// uses 3 bytes... Odd.
uncompressed_file_size : uint24,
struct LZ77Part {
    decision_byte : byte,
    // The next content heavily depends on the decision byte, and I'll represent it with some
    // pseudocode.

    // Go through each bit in decision_byte (High one first, so 1 << 7 to 1 << 0)
    foreach bit in decision_byte:
        // If the bit is zero, we copy a byte from the input.
        // If the bit is one, we reference some data from the input.
        if bit == 0:
            output.add(input.read_byte())
        else
            pointer_data = (input.read_byte() << 8) | input.read_byte();
            length = (pointer_data >> 12) + 3;
            offset = pointer_data & 0xFFF;
            window_offset = output_index - offset - 1;
            for pointByte in range(length):
                output.add(output[windowOffset++])
}

// Read until there is no more input to read.
parts : LZ77Part[]
```

### THE TEXT FORMAT
Text/translation files are compressed with LZ77 and have the following format when uncompressed:
```protobuf
number_of_texts : uint32,
text_addresses : uint32[number_of_texts],

struct Text {
    content : char16[] // Encoding: UTF16
                       // Texts end with a null byte.
};

// Each text address is aligned with its corresponding index in text_addresses
texts : Text[number_of_texts]
```
They're really easy to edit, just remember that the text addresses table has to be updated if the size of any text changes.

### PATCHING
Okay, so imagine we have a few files we have just edited. Some are bigger than the original size, some are smaller. How do we
patch and link these without messing up the entire ROM? Well, here's what the editor does.

First, it treats separate files individually. If you edit one file, a patch will be generated for that single file, and no
modifications will be done to the ROM directly or indirectly via patches. We just keep track of that patch so that we know
that that file has been edited.

When we actually need the updated, patched ROM, we'll generate it from those patches, which involves changing the FAT so
that the file addresses match up, moving the files around, etc. In this case, it's simpler to create the full ROM from
start than patch it from the original, so we just create an empty file, take the header and encryption data and patch the
data files.

