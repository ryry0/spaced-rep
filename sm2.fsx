type Card =
    {
        interval : float
        ease_factor : float
        repetition : uint
    }


let createCard =
    {
        interval = 1.0;
        ease_factor = 2.5
        repetition = (uint)1;
    }

let calcNewEaseFactor prev_ease_factor (quality : uint) =
    let new_ease_factor = 
        prev_ease_factor + (0.1 - (5.0 - (float) quality) * (0.08 + (5.0 - (float) quality) * 0.02))

    if new_ease_factor < 1.3 then
        1.3
    else
        new_ease_factor

//quality 5 is highest, 0 is lowest
let updateCard card (quality : uint) = 
    let new_ease_factor = calcNewEaseFactor card.ease_factor quality
    let new_repetition = card.repetition + 1u
    if quality < 3u then
        { card with repetition = 1u; interval = 1.0 }
    else
        let new_interval =
            match card.repetition with
            | 1u ->
                1.0
            | 2u ->
                3.0
            | _ ->
                card.interval * new_ease_factor |> round

        { interval = new_interval; ease_factor = new_ease_factor; repetition = new_repetition; }


let card = createCard
printfn $"{card}"

let updated_card1 = updateCard card 5u
printfn $"{updated_card1}"

let updated_card2 = updateCard updated_card1 5u
printfn $"{updated_card2}"

let updated_card3 = updateCard updated_card2 5u
printfn $"{updated_card3}"

let updated_card4 = updateCard updated_card3 2u
printfn $"{updated_card4}"

let updated_card5 = updateCard updated_card4 3u
printfn $"{updated_card5}"

let updated_card6 = updateCard updated_card5 3u
printfn $"{updated_card6}"
