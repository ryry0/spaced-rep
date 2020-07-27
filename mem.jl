

#=
Spaced repetition algorithm SM-17

according to the supermemo.guru wiki
1) estimate item difficulty D using history repetitions for the item
2) sdetermine startup stability S0 using the history of repetitions
3) forall repetition history records repeat the following steps
        a) compute the theoretical retrievability Rt using current stability estimate Sw and the interval Int
        b) update recall[] matrix using D, Sw[n-1], Rt with grade derived recall
        c) computer recall-based retrievability Rr
        d) computer grade-derived retrievability Rg
        e) estimate weighted Rw from Rt Rr and Rg
        f) Compute Rw-based stabiity Sr
        g) compute SInc-based Se (Se = Sw[n-1]*SInc[D,Sw[n-1],Rw])
        h) compute interval derived stability Si
        i) estimate weighted Sw from Sr, Se, and Si
        j) compute the stability increase SInc on the basis of Sw change
        k) update Sinc[] mastrx using D, Sw, Rw, with new SInc value
        l) compute new interval using Int:=Sw*SInc[D,Sw,Rw]
        m) Go back to computing Rt step

Replacing Int:=Int[n-1]*SInc[D,Sw,Rw] with Int:=Sw*SInc[D,Sw,Rw] frees the user from ever worring about manual intervention in the length of intervals

=#

function calcSpacedRepetition()
end

