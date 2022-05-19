open Algolia.Search.Clients
open Algolia.Search.Models.Common
open Newtonsoft.Json
open Newtonsoft.Json.Linq
open System.IO

type JustUpc = { Upc: string }

let client = new SearchClient("YourApplicationID", "YourAdminAPIKey")
let index = client.InitIndex("YourIndexName")

let result = index.Browse<JObject>(new BrowseIndexQuery())

let records = new JArray()

for hit in result do
    records.Add(hit)

// Output all records to a JSON file.
let file = File.CreateText(@"results.json")
let serializer = new JsonSerializer()
serializer.Serialize(file, records)



// Map any fields to a type and extract the values you want.
let justUpcs =
    records
    |> Seq.map (fun b -> b.ToObject<JustUpc>())
    |> Seq.map (fun b -> b.Upc)
    |> Seq.sort


File.WriteAllLines("results.txt", justUpcs)


printfn "We did it!"
