# word-counter
A console application that can process an input text file and creates an output file containing the list of all words in the input file and their frequencies.

## Prerequisites

.NET 6.0 SDK - Download [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## How to run

1. Clone the repo
2. `cd WordCounter.CLI`
3. `dotnet run [input-file-path] [output-file-path]`

There are 2 required arguments:
1. input-file-path: A valid path to a file on the machine to read the input text from
2. output-file-path: A valid path on the machine where the output will be written to

Example:

`dotnet run C:\Temp\input.txt C:\Temp\output.txt`