open System.IO

let scissor (a, b) =
    (a * b) - ((min a b) - 1)*10


let outer_loop num_outer num_inner =
    let inner_loop outer_state inner_counter =
        let rec loop_tail acc z =
            match z with
            | z when z <= 0 -> acc
            | p -> loop_tail ((outer_state, p) :: acc) (p - 1)

        loop_tail [] inner_counter

    let rec outer_loop_tail acc z =
        match z with
        | z when z <= 0 -> acc
        | p ->
            let intermediate_list = inner_loop p num_inner
            outer_loop_tail (intermediate_list @ acc) (p - 1)

    outer_loop_tail [] num_outer

let table_list = outer_loop 9 9

let scissor_products = List.map scissor table_list

let stringify (a, b) ans =
    $"\\( {a} \\land {b} = ? \\) ; {ans}\n"

let list_strings =
    List.map2 stringify table_list scissor_products

let one_string = List.reduce (+) list_strings

File.WriteAllText("scissor_table.txt", one_string)

