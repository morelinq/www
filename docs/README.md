# About this Directory

This directory contains Markdown documents with references to source code
snippets that are processed by the [MarkdownSnippets] tool. Such documents
_must_ bear the extension `.source.md`. At build-time, the snippets are
extracted from C# source code files found in the adjacent `code` directory
and embedded into the Markdown documents. The resulting Markdown documents are
then written to the adjacent `docs` directory.


[MarkdownSnippets]: https://github.com/SimonCropp/MarkdownSnippets
