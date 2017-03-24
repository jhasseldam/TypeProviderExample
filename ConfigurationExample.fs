module ConfigurationExample

open FSharp.Configuration
open System

type ConfigurationFile = YamlConfig<"config.yaml">
let conf = ConfigurationFile()

let s = conf.Currencies.USD