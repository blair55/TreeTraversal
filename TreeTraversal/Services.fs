namespace TreeTraversal

open TreeTraversal.Domain
open System.IO
open System

module Services =

    let mutable Cache:Node option = None
    let filePath = Path.GetTempFileName()

    let repository = {
        write = fun p -> let s = sprintf "%i,%s" p.age p.name
                         File.AppendAllLines(filePath, [s])
        read = fun () -> File.ReadLines(filePath)
                         |> Seq.map(fun p -> p.Split(',')) |> Seq.toList
                         |> List.map(fun p -> { age=Convert.ToInt32(p.[0]); name=p.[1] })
    }

    let query repo person =
        match Cache with
            | Some t -> search (t|>Some) person
            | None -> let tree = buildTree (repo.read())
                      Cache <- tree
                      search tree person

    let save repo person =
        repo.write person
        Cache <- None