VAR correctAnswers = 0
VAR gotInstrument = false

Hello there #speaker: Giannakis

*Hello good Sir. I need you to play brass for my new Anthem

You really do? #speaker: Giannakis

**Yes
    ~correctAnswers = correctAnswers + 1

Sure thing! #acquired_instr #speaker: Giannakis

**No

Fuck off! #speaker: Giannakis

- You had {correctAnswers}/1 #speaker: Giannakis

{correctAnswers > 0:
    ~gotInstrument = true
    You have a {gotInstrument} brass #speaker: Giannakis
  - else:
    You have a {gotInstrument} fucking brass #speaker: Giannakis
}

