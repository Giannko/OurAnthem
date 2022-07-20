VAR correctAnswers = 0
VAR gotInstrument = false

Hello there

*Hello good Sir. I need you to play strings for my new Anthem

You really do?

**Yes
    ~correctAnswers = correctAnswers + 1

Sure thing! #acquired_instr

**No

Fuck off!

- You had {correctAnswers}/1

{correctAnswers > 0:
    ~gotInstrument = true
    You have a {gotInstrument} violin
  - else:
    You have a {gotInstrument} fucking violin
}

