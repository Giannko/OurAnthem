VAR correctAnswers = 0
VAR gotInstrument = false

Hello there #speaker: Kostis

*Hello good Sir. I need you to play strings for my new Anthem

You really do? #speaker: Kostis

**Yes
    ~correctAnswers = correctAnswers + 1

Sure thing! #acquired_instr #speaker: Kostis

**No

Fuck off! #speaker: Kostis

- You had {correctAnswers}/1 #speaker: Kostis

{correctAnswers > 0:
    ~gotInstrument = true
    You have a {gotInstrument} violin #speaker: Kostis
  - else:
    You have a {gotInstrument} fucking violin #speaker: Kostis
}

