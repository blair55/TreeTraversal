Instructions

Open the solution with Visual Studio and press F5 to start the web server.
Pacakges may need to be restored by running nuget package restore.

Discussion

I have used the file system as the database as I think the focus of the exercise is
the binary tree and not the persistence mechanism. The file system meets the needs of
the solution as I have designed it. I have defined a Repository type that encapsulates
data access, decoupling the domain from any specific implementation should it need to change.

Each GET request checks the cache first. If the cache is populated the tree is queried.
If the cache is empty the temporary file is read and a balanced tree is constructed,
cached & queried. Every POST request appends the person to a temporay file and empties the
cache such that a new balanced tree can be reconstructed at the next GET request. The
solution is therefore READ optimised, having made the assumption that reads will occur
more often than writes.

The tree is constructed using a pre-order depth first approach. Each node has a unique key
populated by the Person's Age property. Each node also contains a collection of Persons
(people) who's Age is equal to the value of the key. Searching the tree therefore requires
the Node of matching age to be found first, followed by a simple query of the People property.

I have used the CLIMutable Attribute on the Person type so that we can bind to instances
of the type in the Controller Post function.

Give more time, the main improvement I would make would be to make the solution more generic.
Currently, the Node type knows that it's key is an integer and its collection of values 
is a Person type list. The code would be more re-usable as a result.
