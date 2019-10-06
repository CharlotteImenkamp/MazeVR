freq = 1000; 
duration = 0.2; 

%  Snd nur behelfsm‰ﬂig. Sp‰ter auswechseln
beep = MakeBeep(freq,duration);
Snd('Open');
i = 0; 
while i<6
    Snd('Play',beep);
    pause(1);
    i = i+1; 
end 