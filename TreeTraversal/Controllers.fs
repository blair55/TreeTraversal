namespace TreeTraversal

open System
open System.Net
open System.Net.Http
open System.Web.Http
open TreeTraversal.Domain
open TreeTraversal.Services

type PeopleController() =
    inherit ApiController()

    [<Route("people")>]
    member x.Get (request:HttpRequestMessage) (name:string) (age:int) =
        let result = query repository { age=age; name=name }
        match result with
        | Some r -> request.CreateResponse(r)
        | None -> request.CreateResponse(HttpStatusCode.NotFound, "Not Found")

    [<Route("people")>][<HttpPost>]
    member x.Post (request:HttpRequestMessage) (person:Person) =
        let result = save repository person
        request.CreateResponse(HttpStatusCode.Created)