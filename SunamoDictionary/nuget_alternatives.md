# NuGet Alternatives to SunamoDictionary

This document lists popular NuGet packages that provide similar functionality to SunamoDictionary.

## Overview

Dictionary utilities

## Alternative Packages

### System.Collections.Generic.Dictionary
- **NuGet**: System.Collections
- **Purpose**: Built-in dictionary
- **Key Features**: O(1) lookup, key-value pairs

### System.Collections.Concurrent.ConcurrentDictionary
- **NuGet**: System.Collections.Concurrent
- **Purpose**: Thread-safe dictionary
- **Key Features**: Concurrent access, atomic operations

### MoreLINQ
- **NuGet**: morelinq
- **Purpose**: Dictionary extensions
- **Key Features**: ToDictionary variants, grouping operations

### System.Collections.Immutable
- **NuGet**: System.Collections.Immutable
- **Purpose**: Immutable dictionaries
- **Key Features**: Thread-safe, persistent data structures

## Comparison Notes

Dictionary<K,V> for standard use. ConcurrentDictionary for thread safety. ImmutableDictionary for functional programming.

## Choosing an Alternative

Consider these alternatives based on your specific needs:
- **System.Collections.Generic.Dictionary**: Built-in dictionary
- **System.Collections.Concurrent.ConcurrentDictionary**: Thread-safe dictionary
- **MoreLINQ**: Dictionary extensions
