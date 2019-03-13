# MoreLINQ Documentation

This is this repository for [MoreLINQ] documentation.

Documentation is written in Markdown format and built using [MkDocs] and
[MarkdownSnippets].

## Building

The following software is required for building:

- [.NET] Core SDK 2.1 or later
- Docker
- [MarkdownSnippets] 9.0.4 or later

Install MarkdownSnippets as a _local tool_ in the `tools` directory under the
project root:

    dotnet tool install MarkdownSnippets.Tool --version 9.1.0 --tool-path tools

Make sure that the project root is the current working directory of your
command-line shell.

Then if on Windows, run:

    dotnet build && ^
    docker run --rm -it ^
               -v "%cd%:/docs" ^
               --entrypoint /bin/sh ^
               squidfunk/mkdocs-material -c "mkdocs build"

If on Linux or macOS, run instead:

    dotnet build && \
    docker run --rm -it \
               -v "$(pwd):/docs" \
               --entrypoint /bin/sh \
               squidfunk/mkdocs-material -c "mkdocs build"

The resulting web site can be found in the `site` directory.


### Continuous Building

When writing documentation, it is helpful to preview it locally in a browser
and refresh automatically as each document is saved. To do so, open two
command-line shell terminals and change the current working directory of both
to the project root. Then run the following in one of the terminals:

    dotnet watch build

In the second terminal and if on Windows, run:

    docker run --rm -it -v -p 8000:8000 ^
           "%cd%:/docs" squidfunk/mkdocs-material

Otherwise run the following if on Linux or macOS:

    docker run --rm -it -v -p 8000:8000 \
           "$(pwd):/docs" squidfunk/mkdocs-material

Next, open a Web browser and navigate to `http://localhost:8000/`.

Note that due to a bug in `dotnet watch`, new C# source files or Markdown
documents are not detected so after creating such a file, _[touch]_ the
`doc.proj` file or force-save in the editor. There are also known issues with
Docker containers failing to sometimes pick up on file notifications on Windows.
If that seems the case then _touching_ or force-saving the `mkdocs.yml` file
usually fixes the situation.


[MoreLINQ]: https://morelinq.github.io/
[.NET]: https://dot.net/
[MkDocs]: https://www.mkdocs.org/user-guide/deploying-your-docs/
[MarkdownSnippets]: https://github.com/SimonCropp/MarkdownSnippets
[touch]: https://en.wikipedia.org/wiki/Touch_(command)
