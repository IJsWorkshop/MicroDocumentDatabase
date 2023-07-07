# MicroDocumentDatabase

<p>Ever had the need for a small database no fuss nothing complicated just a simple way to store and retreive information when the computer has been reset, welcome to  MicroDocumentDatabase.</p>
<p>This database storage system can be better described as a document type storage system there is no relationship querying like with a conventional databases, MySQL or MSSQL.</p>
<p>Unlike with normal databases you dont have to setup a server to operate this service, it just sits in the local directory and is easy to create, no need to handle complicated querys.</p>
<p>You can compare a table in a normal database as an instance in this one, it also uses the type of the class as part of the filename so you can keep multiple instances of data stored in a directory.</p>
<p>You can use any class you like just tell the database the type when you create the instance of the database.</p>

```C#
var db = new DocumentDriver<MyClassNameHere>();

```

```C
// if you use the data at the end of the readme as temp testing data you will see a filename like this once you have saved it.
// class is called Doc

var db = new DocumentDriver<Doc>();
db.Save();

// looks like this in the filesystem
MicroDatabase_Doc.data

```

<p>Once your have created your database instance you can add to it very easily.</p>

```C#
db.Add(new Doc { id = "a", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
```

<p>Handling data just got easy just use one of the built in commands</p>

```C#

// Save from memory to the filesystem
db.Dump();

// Load to memory from the filesystem
db.Load();

// Get a file from memory
var record = db.Get("b");

// Update
var rec = new Doc { id = "b", name = "Jon", content = "stuff in a record", description = "This is the new information", guid = Guid.NewGuid(), created = DateTime.Now };
db.Update("b", rec);

// Delete
db.Delete("b");

```

<p>its not complicated just have a play around to handle your data. You can modify the code if you want more flexability as the library is very small.</p>

<h1>Support</h1>
<p>Any problems let me know, feel free to use this code in any way you like if u break it you buy it :D</p>


<p>Final thoughts - maybe to help with performance ill release a binary serealiser which would be faster but harder to work with. This library is very easy to keep track of your mistakes as you can look at everything as it all in text even the datastored is in plain text.</p>


<p>Test data for you to play around with</p>

```C#
db.Add(new Doc { id = "a", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "b", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "c", name = "Steve", content = "blah blah blah in a record", description = "asdadadadsadsad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "d", name = "Frank", content = "blah blah blah in a record", description = "asddsadadadsadad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "e", name = "Bobby", content = "blah blah blah in a record", description = "adsasdasdsadasdads", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Simon", content = "blah blah blah in a record", description = "asdasdsadadsadas", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "fs", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "a", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "b", name = "Steve", content = "blah blah blah in a record", description = "asdadadadsadsad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "c", name = "Frank", content = "blah blah blah in a record", description = "asddsadadadsadad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "d", name = "Bobby", content = "blah blah blah in a record", description = "adsasdasdsadasdads", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "e", name = "Simon", content = "blah blah blah in a record", description = "asdasdsadadsadas", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "a", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "b", name = "Steve", content = "blah blah blah in a record", description = "asdadadadsadsad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "c", name = "Frank", content = "blah blah blah in a record", description = "asddsadadadsadad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "d", name = "Bobby", content = "blah blah blah in a record", description = "adsasdasdsadasdads", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "e", name = "Simon", content = "blah blah blah in a record", description = "asdasdsadadsadas", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "a", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "b", name = "Steve", content = "blah blah blah in a record", description = "asdadadadsadsad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "c", name = "Frank", content = "blah blah blah in a record", description = "asddsadadadsadad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "d", name = "Bobby", content = "blah blah blah in a record", description = "adsasdasdsadasdads", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "e", name = "Simon", content = "blah blah blah in a record", description = "asdasdsadadsadas", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "a", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "b", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "c", name = "Steve", content = "blah blah blah in a record", description = "asdadadadsadsad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "d", name = "Frank", content = "blah blah blah in a record", description = "asddsadadadsadad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "e", name = "Bobby", content = "blah blah blah in a record", description = "adsasdasdsadasdads", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Simon", content = "blah blah blah in a record", description = "asdasdsadadsadas", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "fs", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "a", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "b", name = "Steve", content = "blah blah blah in a record", description = "asdadadadsadsad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "c", name = "Frank", content = "blah blah blah in a record", description = "asddsadadadsadad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "d", name = "Bobby", content = "blah blah blah in a record", description = "adsasdasdsadasdads", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "e", name = "Simon", content = "blah blah blah in a record", description = "asdasdsadadsadas", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "a", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "b", name = "Steve", content = "blah blah blah in a record", description = "asdadadadsadsad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "c", name = "Frank", content = "blah blah blah in a record", description = "asddsadadadsadad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "d", name = "Bobby", content = "blah blah blah in a record", description = "adsasdasdsadasdads", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "e", name = "Simon", content = "blah blah blah in a record", description = "asdasdsadadsadas", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "a", name = "Jon", content = "blah blah blah in a record", description = "asdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "b", name = "Steve", content = "blah blah blah in a record", description = "asdadadadsadsad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "c", name = "Frank", content = "blah blah blah in a record", description = "asddsadadadsadad", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "d", name = "Bobby", content = "blah blah blah in a record", description = "adsasdasdsadasdads", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "e", name = "Simon", content = "blah blah blah in a record", description = "asdasdsadadsadas", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });
db.Add(new Doc { id = "f", name = "Jimmy", content = "blah blah blah in a record", description = "asdasdasdasdasdasd", guid = Guid.NewGuid(), created = DateTime.Now });

