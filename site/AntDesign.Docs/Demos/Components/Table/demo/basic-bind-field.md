---
order: 0.2
title:
  en-US: Binding Columns (@bind-Field)
  zh-CN: 基本用法 (@bind-Field)
---

## zh-CN

在 .NET 5 以下版本建议使用 `<Colum>` 的 `@bind-Field` 或 `DataIndex` 来方便地绑定数据。

局限: 使用 `@bind-Feild` 绑定接口类型的实体时，属性必须是引用类型，如 `int` 要改为 `int?`。使用 `DataIndex` 则无此问题。

## en-US

In .NET 5 and later versions, it is recommended to use `@bind-Field` or `DataIndex` for `<Column>` to bind data easily.

Limitation: When using `@bind-Field` to bind an entity of interface type, the property must be a reference type, such as `int` need to be changed to `int?`. Using `DataIndex` does not have this issue.