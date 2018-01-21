

# A 50% duty cycle divide by 3 frequency divider

## Overview
This folder contains an example circuit simulated by MingLogic. It is a 50% duty cycle divide by 3 frequency divider, assuming the input itself is 50% duty cycle. In this document, we will talk about how the circuit is designed.

## Idea
Here is the goal:

```
Input  : 010101010101
Output : 000111000111
```

The idea is to use a counter to count for 3 cycles and repeat, and change the output accordingly, assuming we count from a rising edge to another rising edge for a cycle, here is what the counting would look like:

```
Input  : 010101010101
Count  : 011220011220
Output : 000111000111
```

We can map 0 to 0, 1 to 2, and 2 to 1, but then we will be wrong at the 6th bit.

```
Input  : 010101010101
Count  : 011220011220
Mapped : 000110000110
Output : 000111000111
```

Think more about it, it is obvious that no mapping could possibly work. The key problem is that the falling edge of the output is a falling edge of the input, and if we only count of rising edge, there is no way we can induce in the output. That leads to idea of doing both the rising edge counter and the falling edge counter.

```
Input  : 010101010101
Count1 : 011220011220
Count2 : 001122001122
Output : 000111000111
```

Now it is obvious, we can set the output to be 0 if both count1 and count2 does not have a 2, otherwise we set it to 1, and we can get the perfect output we wanted.


## Implementation
The logic simulator we have is pretty primitive, we basically need to build almost everything from scratch. We constructed the AND, OR, NOT, XOR gate and the D flip flop. These are standard components.

Here is the application specific component. Because we want to easily detect the 2, therefore, instead of encoding it as `10`, we encode it as `11` instead, so that it can be easily detected by an AND gate. Therefore the state transition sequence is `00 -> 01 -> 11`.

To implement the first bit, we have to implement the function:
```
00 -> 1
01 -> 1
11 -> 0
```
The can be implemented with a simple NAND gate.

To implement the second bit, we have to implement the function
```
00->0
01->1
11->0
```
This can be implemented with a XOR gate.

That concludes the design for the counter in the `counter.json`.  We have an extra enable signal to make sure the flip-flops are effectively set zero before we start the counting.

Given the counter - we can implement the frequency divider. We instantiate the counter twice, one with the clock signal and another with the inverted clock signal to produce the two counters. Two and gates are used to detect whether we have a `11` in the counters, and we use an or gate to produce the final output. 

That concludes the frequency divider design.