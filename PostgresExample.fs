module PostgresExample

open FSharp.Data.Sql
open System
open Npgsql
open System

let [<Literal>] private connstring = "Server= Y30591; Port = 5432; User Id = postgres; Password = admin;  Database = auth";
// --------------- SqlProvider -----------------------

type Sql = SqlDataProvider<ConnectionString = connstring, // <--- Compile time model representation. Requires a working database 
                            DatabaseVendor = Common.DatabaseProviderTypes.POSTGRESQL>
let private context =
  Sql.GetDataContext(connstring) // <-- Runtime connection. Does not require a working database

let getRoles username =
  query { for u in context.Public.FxcoreUser do
          join ur in context.Public.UserRole on (u.Id = ur.FxcoreUser)
          join r in context.Public.Role on (ur.Role = r.Id)
          where (u.Name = username)
          select r.Name
        } |> Seq.toList

// --------------- Npgsql -----------------------
let getRoles' user =
  let query =
    sprintf "select r.name from role r \
    join user_role ur on ur.role = r.id \
    join fxcore_user u on ur.fxcore_user = u.id \
    where u.name = '%s'" user
  use conn = new NpgsqlConnection(connstring)
  use cmd = new NpgsqlCommand(query, conn)
  conn.Open()
  [use reader = cmd.ExecuteReader()
  while reader.Read() do
  yield (reader.GetString(0))]
