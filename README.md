# Resources Data Generator for Unity

## Description
A Unity script that automatically generates a C# `ResourcesData` class based on the structure of the `Resources` folder. Each folder becomes a separate class (with nesting support), and files are converted into static properties using `UnityEngine.Resources.Load`.

## Problem
Accessing resources in Unity via `Resources.Load` requires string paths, which can lead to:
- Errors due to typos in paths.
- Inconvenience during folder structure refactoring.
- Lack of autocompletion and type checking in code editors.

This script solves these issues by automating the creation of a type-safe class for resource access.

## Features
- Traverses all nested folders in `Resources`.
- Generates nested classes corresponding to the folder structure.
- Creates static properties for files with their respective types (e.g., `Texture2D`, `Font`).
- Automatically determines resource paths and types for `Resources.Load`.
- Formats file names into camelCase for valid property names.

## Installation
1. Copy the `ResourcesHelper.cs` script into your Unity project’s `Editor` folder.
2. Ensure the `Resources` folder contains the necessary resources.

## Usage
1. Place resources (e.g., textures, fonts) in the `Resources` folder.
2. Call `ResourcesHelper.GenerateCode()` via the Unity menu or configure for automatic execution.
3. The generated `ResourcesData.cs` file will appear in `_App/Scripts/Data`.
4. Use the `ResourcesData` class to access resources, e.g., `ResourcesData.Test_0.Data.Visual.Circle`.

## Notes
- Only resource types compatible with `Resources.Load` are supported.
- File names are automatically converted to camelCase, excluding invalid characters.
- The generated code is updated when the script is rerun.
- Ensure the `_App/Scripts/Data` folder exists or will be created (or specify your path).

---

## Описание
Скрипт для Unity, который автоматически генерирует C# класс `ResourcesData` на основе структуры папки `Resources`. Каждая папка становится отдельным классом (с поддержкой вложенности), а файлы преобразуются в статические свойства с использованием `UnityEngine.Resources.Load`.

## Проблема
В Unity доступ к ресурсам через `Resources.Load` требует указания строковых путей, что приводит к:
- Ошибкам из-за опечаток в путях.
- Неудобству при рефакторинге структуры папок.
- Отсутствию автодополнения и проверки типов в редакторе кода.

Данный скрипт решает эти проблемы, автоматизируя создание типобезопасного класса для доступа к ресурсам.

## Возможности
- Обход всех вложенных папок в `Resources`.
- Генерация вложенных классов, соответствующих структуре папок.
- Создание статических свойств для файлов с указанием их типов (например, `Texture2D`, `Font`).
- Автоматическое определение пути и типа ресурсов для `Resources.Load`.
- Форматирование имен файлов в camelCase для корректных имен свойств.

## Установка
1. Скопируйте скрипт `ResourcesHelper.cs` в папку `Editor` вашего проекта Unity.
2. Убедитесь, что папка `Resources` содержит необходимые ресурсы.

## Использование
1. Поместите ресурсы (например, текстуры, шрифты) в папку `Resources`.
2. Вызовите `ResourcesHelper.GenerateCode()` через меню Unity или настройте автоматический запуск.
3. Сгенерированный файл `ResourcesData.cs` появится в `_App/Scripts/Data`.
4. Используйте класс `ResourcesData` для доступа к ресурсам, например: `ResourcesData.Test_0.Data.Visual.Circle`.

## Примечания
- Поддерживаются только типы ресурсов, совместимые с `Resources.Load`.
- Имена файлов автоматически преобразуются в camelCase, исключая недопустимые символы.
- Сгенерированный код обновляется при повторном запуске скрипта.
- Убедитесь, что папка `_App/Scripts/Data` существует или будет создана (или укажите свой путь).

---
