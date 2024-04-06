﻿namespace PokeDexSharp.PokeApiLayer;

[PkResource("move-ailment/([0-9]{1,})")]
public class PkMoveAilment : PkLocalizedBase
{
    [JsonPropertyName("moves")]
    public PkResource[] Moves { get; set; } = [];
}
