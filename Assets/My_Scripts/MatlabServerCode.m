clc
clear all
tcpipServer = tcpip('localhost',55000,'NetworkRole','server');
while(1)
data = membrane(1);
fopen(tcpipServer);
rawData = fread(tcpipServer,14,'char');
for i=1:14 rawwData(i)= char(rawData(i));
end
fclose(tcpipServer);
end

        echotcpip('on',4012) 
        t = tcpip('localhost',4012); 
        fopen(t)
        fwrite(t,65:74) 
        A = fread(t, 10); 
        fclose(t);
        delete(t)
        echotcpip('off')