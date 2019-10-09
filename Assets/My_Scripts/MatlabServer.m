
clc
clear all;

% toolpath
 toolpath = 'C:\Program Files\MATLAB\toolbox\';
 addpath(genpath([toolpath,'Psychtoolbox']));
 sca;

%% set up the code for communicating with Unity
tcpipServer = tcpip('127.0.0.1',55001,'NetworkRole','server');

%% Timeout to complete read and write operations in seconds. default is 10
%%set(tcpipServer,'Timeout',3);       %auch als Server?

%% Connection timeout value. Maximum time in seconds to wait for a
% connection request
% set(tcpipServer,'ConnectTimeout',60);      

% set a flag to close the while loop
flag = 1;

% initiate the while loop 
while true
    
    % stays here until a connection is there
    disp("Server open");
    fopen(tcpipServer);
    flag = 0;
    %     data = fread(tcpipServer,tcipServer.BytesAvailable);
    %     disp("Server write");
    %     fwrite(tcpipServer,"here is your personal server");
    disp("server close");
    fclose(tcpipServer);
    NoveltyOddball();
    
    
    
end 
    

