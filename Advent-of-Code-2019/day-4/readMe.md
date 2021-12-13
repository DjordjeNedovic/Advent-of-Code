### --- Day 4: Secure Container ---

You arrive at the Venus fuel depot only to discover it's protected by a
password. The Elves had written the password on a sticky note, but someone
threw it out.

However, they do remember a few key facts about the password:

- It is a six-digit number.
- The value is within the range given in your puzzle input.
- Two adjacent digits are the same (like `22` in `1`**`22`**`345`).
- Going from left to right, the digits **never decrease**; they only ever 
increase or stay the same (like `111123` or `135679`).

Other than the range rule, the following are true:

- `111111` meets these criteria (double 11, never decreases).
- `2234`**`50`** does not meet these criteria (decreasing pair of digits `50`).
- `123789` does not meet these criteria (no double).

**How many different passwords** within the range given in your puzzle 
input meet these criteria?

<details>
  <summary><strong><em>See result here</em></strong></summary>
		Your puzzle answer was <strong><em>2090</em></strong>.
</details>

---

### --- Part Two ---

An Elf just remembered one more important detail: the two adjacent matching
digits *are not part of a larger group of matching digits*.

Given this additional criterion, but still ignoring the range rule, the
following are now true:

- `112233` meets these criteria because the digits never decrease and all repeated digits are exactly two digits long.
- `123`**`444`** no longer meets the criteria (the repeated `44` is part of a larger group of `444`).
- `111122` meets the criteria (even though 1 is repeated more than twice, it still contains a double `22`).

**How many different passwords** within the range given in your puzzle input meet all of the criteria?

<details>
  <summary><strong><em>See result here</em></strong></summary>
		Your puzzle answer was <strong><em>1419</em></strong>.
</details>


--------------------------------
### Additional help

<details>
  <summary><strong><em>Open to see</em></strong></summary>
		Thanks to this comment:
		https://www.reddit.com/r/adventofcode/comments/e65jgt/2019_day_4_part_2_am_i_misunderstanding_the_given/fctulbn?utm_source=share&utm_medium=web2x

		Additional test cases:

		assert.equal(hasDupe("123444"), *false*);  
		assert.equal(hasDupe("124444"), *false*);  
		assert.equal(hasDupe("113334"), *true*);  
		assert.equal(hasDupe("111334"), *true*);  
		assert.equal(hasDupe("113345"), *true*);  
		assert.equal(hasDupe("111122"), *true*);  
		assert.equal(hasDupe("112233"), *true*);  
		assert.equal(hasDupe("123445"), *true*);  
		assert.equal(hasDupe("123456"), *false*);
</details>
