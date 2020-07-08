## INTRODUCTION
I wanted to create a simple editor I could use to change texts in Mario Party DS without messing around
with 5000 different files, editors and decompression tools.
Instead of directly modifying the ROM like other editors, this one generates patch files for every single
file change, making single-file revertions and source control much, much easier to accomplish. This also
means you won't need a trillion different NDS files or full patches to distinguish your ROM versions.

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
we don't change the size of the total filetable or nametable. This is because the filetable is stored
outside of the header, but its size and address of it are stored in the header.

More specifically:

   Object       |   Address   |  Size
----------------|-------------|-------------
File Name Table | Header 0x40 | Header 0x44
FAT             | Header 0x48 | Header 0x4C

For simplicity, let's assume these values are constant. This means that Mario Party DS has these constant
values:

   Object       |   Address   |  Size
----------------|-------------|--------------------------------
File Name Table | 0x00308800  | 0x00003479 Bytes (13433 Bytes)
FAT             | 0x0030BE00  | 0x00001610 Bytes (5648 Bytes)

We are free to change the values contained in these partitions as much as we want.

### THE TEXT FORMAT
Text/translation files are compressed with LZ77 compression and have the following format:
```protobuf
number_of_texts : uint32,
text_addresses : uint32[number_of_texts],

struct text {
    content : char16[] // Encoding: UTF16
                       // Texts end with a null byte.
};

// Each text address is aligned with its corresponding index in text_addresses
texts : text[number_of_texts]
```
They're really easy to edit, just remember that the text addresses table has to be updated if the size of any text changes.