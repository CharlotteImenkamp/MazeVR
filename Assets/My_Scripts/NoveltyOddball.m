try     
% close all; clear all;
% fclose all; clc; 

% toolpath
 toolpath = 'C:\Program Files\MATLAB\toolbox\';
 addpath(genpath([toolpath,'Psychtoolbox']));
 sca;
 
%************************************************************************** 
% variables
AnzTrial = 10; 
AnzBlock = 1; 
percS = 0.8; 
percD = 0.1; 
percN = 0.1; 
switch_txtdatei = 0; 
Subject = '4';
%diraudiofiles = 'H:\GitHub\MazeVR\Assets\My_Sounds\stimuli\sounds\Top60_familiarity\';
diraudiofiles = 'C:\Users\Charlotte\Documents\GitHub\MazeVR\Assets\My_Sounds\stimuli\sounds\Top60_familiarity\';
dirtextfile = 'H:\GitHub\';
%**************************************************************************
 
% instantiate the library
%  lib = lsl_loadlib();                                                       % Loading library...
%  info = lsl_streaminfo(lib,'MyMarkerStream',...
%  'Markers',1,0,'cf_string','myuniquesourceid23443');                        % Creating a new marker stream info...
%  outlet = lsl_outlet(info);                                                 % Opening an outlet...
%    markers = {'1', '2', '3'};                                               % Now transmitting data... % 1 = Standard 2 = Deviant 3 = Novelty

% audio
InitializePsychSound(1);                                                                                        
waitForDeviceStart = 1;
soundfiles = dir([diraudiofiles '*.wav']);
freqaudio = 22050;                                                          
pahandle = PsychPortAudio('Open', 2, 1, 4, freqaudio);                      % Die erste 1 mit einer 9 ersetzen!!!!! PsychPortAudio('Open' [, deviceid][, mode (1= sound playback only)][, reqlatencyclass]
PsychPortAudio('Volume', pahandle, 1.1);                                    % alternativ: 0.5

tonS = MakeBeep (600,1,freqaudio);
tonD = MakeBeep (1200,1,freqaudio);
tonD = tonD.*0.8;
tonE = tonD.*0; 

% silent beep for audioport
PsychPortAudio('FillBuffer', pahandle, [tonE; tonE]);
pause(0.1)
PsychPortAudio('Start', pahandle, 0.15, 0, waitForDeviceStart);

% *************************************************************************

% .txt file - data
if switch_txtdatei
    fid = fopen([dirtextfile Subject '.txt'],'wt');
    fprintf(fid, ['Proband ' Subject ' :\n'] );
    fprintf(fid, ['Anzahl Trials: ' num2str(AnzTrial) '\n'] );
    fprintf(fid, ['Anzahl Blöcke: ' num2str(AnzBlock) '\n'] );
    fprintf(fid, ['Standard: ' num2str(percS*100) ' %\n'] );
    fprintf(fid, ['Standard: ' num2str(percD*100) ' %\n'] );
    fprintf(fid, ['Standard: ' num2str(percN*100) ' %\n'] );
end

% block loop 
for m = 1:AnzBlock
    AnzD = 0;
    AnzN = 0;
    tmpN = 1;
    orderFiles = randperm(size(soundfiles,1));
    orderTrial = mkOrder_ci(AnzTrial, percS, percD, percN);
    
    %    txt start
    if switch_txtdatei
        fprintf(fid, '\n');
        fprintf(fid, ['Block ' num2str(m) ' :\n']);
    end   
    
    %     trial loop
    for n = 1:AnzTrial
        
        tpause = 1+ randperm(40,1)/100; 
        pause (tpause)                                                     % range from 1 to 1.40s
        
%        Standard                                                      
        if orderTrial(n) == 1 
%             mrk = markers{1};
            PsychPortAudio('FillBuffer', pahandle, [tonS; tonS]);
            pause(0.1)
            PsychPortAudio('Start', pahandle, 0.15, 0, waitForDeviceStart);
%             outlet.push_sample({mrk});
            if switch_txtdatei
                fprintf(fid, ['Trial ' num2str(n) ' : S\n']);
            end
%        Deviant
        elseif orderTrial(n) == 2 
%             mrk = markers{2};
            PsychPortAudio('FillBuffer', pahandle, [tonD; tonD]);
            pause(0.1)
            PsychPortAudio('Start', pahandle, 0.15, 0, waitForDeviceStart);
%             outlet.push_sample({mrk});
              AnzD = AnzD + 1 ;  
              if switch_txtdatei
                  fprintf(fid, ['Trial ' num2str(n) ' : D\n']);
              end
              
%         Novelty
        elseif orderTrial(n) == 3
%             mrk = markers{3};
            
            if tmpN > size(soundfiles,1)
                tmpN = 1;
                orderFiles = randperm(size(soundfiles,1));
            end
            
            presentfile = soundfiles(orderFiles(tmpN)).name;
            
            [audiodata] = audioread([diraudiofiles presentfile]);                          
            PsychPortAudio('FillBuffer', pahandle, [audiodata'; audiodata']);
            pause(0.1)
            PsychPortAudio('Start', pahandle);
%             outlet.push_sample({mrk});
            
            AnzN= AnzN+1;
            tmpN = tmpN +1;
            if switch_txtdatei
                fprintf(fid, ['Trial ' num2str(n) ' : N (' presentfile ')\n']);
            end 
            
        else 
            disp ('Fehler bei Pseudorandomisierung');
        end
    end
    pause(1.45)
     
     % number deviant/novelty
     disp('********************');
     disp(['Anzahl Deviants : ' num2str(AnzD) ]);           
     disp(['Anzahl Novelties: ' num2str(AnzN) ]);
     disp('********************');
end

if switch_txtdatei
    fclose(fid);
end

catch
    fclose('all');
    psychrethrow(psychlasterror);                                          
end 
