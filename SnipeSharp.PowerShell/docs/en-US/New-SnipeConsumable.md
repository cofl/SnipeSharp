﻿---
external help file: SnipeSharp.PowerShell.dll-Help.xml
Module Name: SnipeSharp.PowerShell
online version:
schema: 2.0.0
---

# New-SnipeConsumable

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

```
New-SnipeConsumable [-Name] <String>
 -Category <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Category]> [-Quantity] <Int32>
 [-Company <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Company]>]
 [-ItemNumber <String>]
 [-Location <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Location]>]
 [-Manufacturer <SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Manufacturer]>]
 [-MinimumQuantity <Int32>] [-ModelNumber <String>] [-OrderNumber <String>] [-PurchaseCost <Decimal>]
 [-PurchaseDate <DateTime>] [-IsRequestable <Boolean>] [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Category
{{Fill Category Description}}

```yaml
Type: SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Category]
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Company
{{Fill Company Description}}

```yaml
Type: SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Company]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IsRequestable
{{Fill IsRequestable Description}}

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ItemNumber
{{Fill ItemNumber Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Location
{{Fill Location Description}}

```yaml
Type: SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Location]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Manufacturer
{{Fill Manufacturer Description}}

```yaml
Type: SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[SnipeSharp.Models.Manufacturer]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -MinimumQuantity
{{Fill MinimumQuantity Description}}

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ModelNumber
{{Fill ModelNumber Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Name
{{Fill Name Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -OrderNumber
{{Fill OrderNumber Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -PurchaseCost
{{Fill PurchaseCost Description}}

```yaml
Type: Decimal
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -PurchaseDate
{{Fill PurchaseDate Description}}

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Quantity
{{Fill Quantity Description}}

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[[SnipeSharp.Models.Category, SnipeSharp, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null]]

### System.Int32

### SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[[SnipeSharp.Models.Company, SnipeSharp, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null]]

### SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[[SnipeSharp.Models.Location, SnipeSharp, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null]]

### SnipeSharp.PowerShell.BindingTypes.ObjectBinding`1[[SnipeSharp.Models.Manufacturer, SnipeSharp, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null]]

### System.Decimal

### System.DateTime

### System.Boolean

## OUTPUTS

### SnipeSharp.Models.Consumable

## NOTES

## RELATED LINKS