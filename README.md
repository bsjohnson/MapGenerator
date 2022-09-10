# Dungeosis
A maze generator for C#.  The primary algorithm for generating this maze was adapted from this fantastic tutorial by Bob Nystrom, which can be found [here](https://journal.stuffwithstuff.com/2014/12/21/rooms-and-mazes/).

## Why C#?
A couple reasons:
1. I was mostly unfamiliar with C#, and this was a learning opportunity to build a Console and Web-based version of the generator in a language I've rarely worked with in professional settings.  Different languages and different platforms can tickle different parts of the brain; it was a nice deviation from normal work.
2. While learning [Unity](https://unity.com/), I wanted something I could adapt and plug in to the tutorials to visualize the generated map.  Taking the resulting map array and converting to a performant 3D model was more challenging than just rendering cubes, exposing potential performance problems with this method of generation early on.

