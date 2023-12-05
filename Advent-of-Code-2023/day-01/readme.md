Text of the day can be found here:
https://adventofcode.com/2023/day/1

### PART ONE

For the first part, I used regex to find just numbers in the string (`\d+`), take the first and the last match, and if there are more digits than one, take the first/last digit, create a number with string concatenation, parse into int and add to result sum.

### PART TWO
I started the same as the first one, with regex, but I found an issue with parsing `oneight`, so I decided to regex just for numbers and `String.Contains()` for words.

### Good look!
