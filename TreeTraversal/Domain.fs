namespace TreeTraversal

open System

module Domain =

    [<CLIMutable>]
    type Person = { name:string; age:int }
    type Node = { key:int; left:Node option; right:Node option; people:Person list }
    type Repository = { write:Person->unit; read:unit->Person list }

    let leaf key people = { key=key; left=None; right=None; people=people }
    let withLeft node left = { node with left=Some left }
    let withRight node right = { node with right=Some right }
    let append node people = { node with people=people@node.people }

    let rec insertRec node key people =
        if key < node.key then
            match node.left with
            | Some n -> insertRec n key people |> withLeft node
            | None -> leaf key people |> withLeft node
        elif key > node.key then
            match node.right with
            | Some n -> insertRec n key people |> withRight node
            | None -> leaf key people |> withRight node
        else append node people
        
    let insert node key people =
        match node with
        | Some n -> insertRec n key people
        | None -> leaf key people

    let rec buildTreeRec groups node =
        match groups with
        | [] -> node
        | _  -> let (mkey, mgroup) = groups.[groups.Length/2]
                let nM = insert node mkey (mgroup |> Seq.toList) |> Some
                let (left, right) =
                    groups
                    |> List.filter(fun (key, _) -> key <> mkey)
                    |> List.partition(fun (key, _) -> key < mkey)
                let nL = buildTreeRec left nM
                let nR = buildTreeRec right nL
                nR

    let buildTree people =
        let sortedGroups =
            people
            |> Seq.groupBy(fun p -> p.age)
            |> Seq.sortBy(fun (key, _) -> key)
            |> Seq.toList
        buildTreeRec sortedGroups None

    let rec search node person =
        match node with
        | None -> None
        | Some n ->
            if person.age < n.key then search n.left person
            elif person.age > n.key then search n.right person
            else n.people |> List.tryFind(fun p -> p = person)
