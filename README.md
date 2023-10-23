<h1 align="center">Aimlabs Miss Sounds</h1>

<p align="center">Plays a sound when you miss your shots in Aimlabs</p>

<br>

## Motivation

Feedback plays a critical role in our brain's ability to learn. Unfortunately, Aimlabs doesn't provide negative feedback when you miss, which has been shown to significantly speed up the process of motor learning. [1][2]

Anecdotally, I noticed myself that I don't recognize bad habits due to this lack of feedback. Instead requiring a conscious effort that distracts from aiming.

1: [The dissociable effects of punishment and reward on motor learning](https://www.nature.com/articles/nn.3956)<br>
2: [Neurocognitive Mechanisms of Error-Based Motor Learning](https://link.springer.com/chapter/10.1007/978-1-4614-5465-6_3)

<br>

## Installation

1. Get the [interception driver](https://github.com/oblitum/Interception) to detect mouse clicks
2. [Get and run the application](/releases)
3. Click dots (or miss them :P)

### Considerations

- Make sure Aimlabs has no other sounds enabled than hit sounds. The program relies on hit sounds to tell if we've actually hit
- You can customise your own miss sound by replacing the miss.wav

<br>

Also, Valorant blocks the interception driver, sorry about that x) I'll try to remove the requirement in a future version.

<br>

## Limitations

- I haven't added miss sounds for track aim yet, it detects single clicks only
- There's a 100ms delay before a miss sound plays because Windows' audio API is hella slow, but I plan to work around that soon

<br>

## Development

If you wanna compile from scratch or modify the source code, make sure you have .NET framework v4.7.2 installed. I like developing in the CLI, so you can use `run.cmd` to build and deploy in one go.

<br>

## License

[MIT](LICENSE)
