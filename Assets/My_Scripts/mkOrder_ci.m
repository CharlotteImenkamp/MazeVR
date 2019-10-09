function Order = mkOrder_ci(AnzTrial, percS, percD, percN)
% Funktion zum Ordnen der Töne nach bestimmten Kriterien
% Eingegeben werden muss die Anzahl der Durchläufe (AnzTrial) und die
% Prozentangaben der Bestandteile

if percS*100 + percD*100 + percN*100 == 100
    AnzS = AnzTrial * percS;
    AnzD = AnzTrial * percD;
    AnzN = AnzTrial * percN;
    
    S = ones(1,AnzS);
    D = ones(1,AnzD).*2;
    N = ones(1,AnzN).*3;
    
    maxAnz = mode([S D N]);
    
    check = false;
    Ncheck = 0;
    
    while check == false && Ncheck <3                                       % Falls 20 Iterationen nicht ausreichen, nochmal mischen.
                                                                            % Falls nach 2 weiteren Versuchen kein Ergebnis dann Fehlermeldung 
        Order = Shuffle([S, D, N]);
        
        swap = true;
        NLoop = 0;
        
        %     start loop
        while swap == true && NLoop < 20
            swap = false;
            NTausch = 0;
            NEins = 2;
            
            %        first cell = standard
            while Order(1) ~= maxAnz
                temp = Order(1);
                Order(1) = Order(NEins);
                Order(NEins) = temp;
                
                NEins = NEins + 1;
            end
            
            %        loop through cells
            for i= 2:1:(AnzTrial-1)
                if Order(i) ~= maxAnz && Order(i+1) ~= maxAnz
                    temp = Order(i);
                    Order(i) = Order(i-1);
                    Order(i-1) = temp;
                    
                    swap = true;
                    NTausch = NTausch + 1;
                end
            end
            
            %       end loop
            NLoop = NLoop+1 ;
        end
        check = true;
        
        %    maximum iteration bound
        if NLoop == 20
            check = false;
            Ncheck = Ncheck + 1;
            
            fprintf(2,'***Achtung: Iterationsgrenze erreicht. Neuer Versuch startet. \n');
        end 
    end
    
    if Ncheck == 3
        fprintf(2, '***Achtung: Anforderungen nicht möglich. Parameter ändern \n')
    end
    
else
    fprintf(2,'***Achtung: Die Verhältnisse der Komponenten ergeben nicht 1! ');
end

end

