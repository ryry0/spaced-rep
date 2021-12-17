open System.IO

let front_template title range1 range2 op =
    $"""
<p id="output">{title}</p>
<script>
String.prototype.hashCode = function() {{
  var hash = 0, i, chr, len;
  if (this.length === 0) return hash;
  for (i = 0, len = this.length; i < len; i++) {{
    chr   = this.charCodeAt(i);
    hash  = ((hash << 5) - hash) + chr;
    hash |= 0; /* Convert to 32bit integer */
  }}
  return hash;
}};
seed = function(s) {{
    s+=new Date().getDate();
    return function() {{
        s = Math.sin(s) * 10000; return s - Math.floor(s);
    }};
}};

var range1 = {range1};
var range2 = {range2};
var xs = seed("{title}".hashCode())();
var x = Math.floor(xs*range1);
var y = Math.floor((seed(xs)()*range2));
var ans = x {op} y;

document.getElementById("output").innerHTML = x + "{op}" + y + "=?";
</script> @
    """

let get_title_op op =
    match op with
    | '+' -> "add"
    | '-' -> "sub"
    | '*' -> "mul"
    | '/' -> "div"
    | _ -> "error"

let gen_template template digits1 digits2 op (num: int) : string=
    let title_op =
        get_title_op op

    let full_title =
        $"{title_op}{digits1}by{digits2}:{num}"

    let range1, range2 =
        10.0 ** digits1, 10.0 ** digits2

    template full_title range1 range2 op


let back_template title range1 range2 op =
    $"""
<p id="output"></p>
<script>
String.prototype.hashCode = function() {{
  var hash = 0, i, chr, len;
  if (this.length === 0) return hash;
  for (i = 0, len = this.length; i < len; i++) {{
    chr   = this.charCodeAt(i);
    hash  = ((hash << 5) - hash) + chr;
    hash |= 0; /* Convert to 32bit integer */
  }}
  return hash;
}};
seed = function(s) {{
    s+=new Date().getDate();
    return function() {{
        s = Math.sin(s) * 10000; return s - Math.floor(s);
    }};
}};

var range1 = {range1};
var range2 = {range2};
var xs = seed("{title}".hashCode())();
var x = Math.floor(xs*range1);
var y = Math.floor((seed(xs)()*range2));
var ans = x {op} y;

document.getElementById("output").innerHTML = x + "{op}" + y + "=" + ans;
</script>
    """

let gen_full_card digits1 digits2 op num =
    let front =
        gen_template front_template digits1 digits2 op num
    let back =
        gen_template back_template digits1 digits2 op num

    let full_card = front + back
    full_card.Replace("\r", "").Replace("\n","") + "\n"

let gen_deck digits1 digits2 op num =
    [1..num]
        |> List.map (gen_full_card digits1 digits2 op)
        |> List.reduce (+)

let write_deck digits1 digits2 op num =
    let title_op = get_title_op op
    File.WriteAllText($"{title_op}{digits1}by{digits2}-{num}cards.txt", gen_deck digits1 digits2 op num)

write_deck 2 2 '+' 5
write_deck 2 2 '-' 5
write_deck 2 2 '*' 5
write_deck 2 2 '/' 5
