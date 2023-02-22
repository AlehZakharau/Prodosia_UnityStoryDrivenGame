# Prodosia_UnityStoryDrivenGame
Prodosia is a quiz game featuring a professional voice-over, created specifically for Ludum Dare 49.

## Description

Prodosia is a horror story-driven quiz game that was created for Ludum Dare 49. The project features professional voice-over and achieved good results in the jam, taking second place in the audio category.

Plot: Many years ago, we discovered a Goddess beneath the earth. With her guidance, we entered a Golden Age: wars ended, non-discriminatory laws were established, governments were disestablished, and we ventured into the stars, where we reside to this day. The Goddess became the mother to all people, but some chose to remain as orphans.

[https://alehzaharau.itch.io/ludum-dare-49](https://alehzaharau.itch.io/ludum-dare-49)

I have fully refactored the project and added new features to the game:

- "Skip Line" feature that allows the user to skip the current audio and move on to the next. This feature is helpful for testing and will also be useful during replay sessions.
- Configurable "Script" that includes the entire story in one file, with the ability to add text and audio, add events on audio, and choose all questions, etc.
- Added effects to help the player understand whether their answers are right or wrong.

In the technical design of this project, the entire logic is initiated from "EntryPoint.cs". In this file, I manually initialize all classes and provide their dependencies.

To call certain methods without providing dependencies, I utilize my custom event bus on structs.

Although I used the localization library from a previous implementation, I had to refactor it to meet the specific needs of this project.
