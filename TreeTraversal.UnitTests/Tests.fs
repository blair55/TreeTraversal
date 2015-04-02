namespace TreeTraversal.UnitTests

open NUnit.Framework
open FsUnit
open TreeTraversal.Domain

module UnitTests =

    let jim = { name="jim"; age=23 }
    let bob = { name="bob"; age=30 }
    let tim = { name="tim"; age=27 }
    let jon = { name="jon"; age=27 }
    let tom = { name="tom"; age=24 }
    let people = [ jim; bob; tim; jon; tom ]
    
    [<Test>]
    let ``tree is constructed as expected``() =

        // arrange
        let expected = {
            key=27
            left= { key=24
                    left={ key=23
                           left=None
                           right=None
                           people=[jim] } |> Some
                    right=None
                    people=[tom] } |> Some
            right={ key=30
                    left=None
                    right=None
                    people=[bob] }  |> Some
            people=[tim;jon] } |> Some
        
        // act
        let result = buildTree people

        // assert
        result |> should equal expected 
        

    [<Test>]
    let ``search successfully finds existing person``() =
        
        // arrange
        let tree = buildTree people
        
        //act
        let result = search tree { name="jon"; age=27 }
        
        // assert
        result |> should equal (jon|>Some)

    
    [<Test>]
    let ``search returns None for non-existing person``() =
        
        // arrange
        let tree = buildTree people
        
        //act
        let result = search tree { name="i don't exist"; age=27 }
        
        // assert
        result |> should equal (None)