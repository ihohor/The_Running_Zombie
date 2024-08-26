using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ZombieGravestone : MonoBehaviour
{
    // Tablica z przykładowymi imionami
    private string[] zombieNames = new string[] {
    // Zombie names
    "Zed", "Rotface", "Gore", "Fleshgnawer", "Gravewalker", "Skullsplitter", "Boney", "Smasher", "Stinky", "Rotten",

    // Polska
    "Jan", "Piotr", "Paweł", "Michał", "Krzysztof", "Tomasz", "Marek", "Adam", "Jakub", "Wojciech",

    // Niemcy
    "Hans", "Karl", "Stefan", "Max", "Lukas", "Felix", "Jonas", "Paul", "Leon", "Michael",

    // Francja
    "Jean", "Pierre", "Louis", "Antoine", "François", "Émile", "Julien", "Gabriel", "Lucas", "Théo",

    // Włochy
    "Giovanni", "Marco", "Luca", "Matteo", "Andrea", "Giuseppe", "Stefano", "Davide", "Alessandro", "Francesco",

    // Hiszpania
    "Juan", "Carlos", "Javier", "Miguel", "Luis", "Alejandro", "Pablo", "Diego", "Manuel", "Antonio",

    // Portugalia
    "João", "Pedro", "José", "Miguel", "António", "Tiago", "Rafael", "David", "Carlos", "Luís",

    // Rosja
    "Ivan", "Dmitri", "Alexei", "Sergei", "Mikhail", "Vladimir", "Pavel", "Nikolai", "Boris", "Oleg",

    // Ukraina
    "Oleksandr", "Andriy", "Dmytro", "Ihor", "Serhiy", "Yuriy", "Mykola", "Volodymyr", "Vadym", "Orest",

    // Szwecja
    "Erik", "Lars", "Karl", "Johan", "Magnus", "Stefan", "Anders", "Henrik", "Björn", "Oskar",

    // Norwegia
    "Ole", "Lars", "Erik", "Morten", "Knut", "Sindre", "Bjørn", "Espen", "Magnus", "Kristian",

    // Finlandia
    "Juhani", "Matti", "Mikko", "Juho", "Antti", "Heikki", "Kari", "Markus", "Timo", "Sami",

    // Dania
    "Mikkel", "Søren", "Niels", "Jens", "Frederik", "Christian", "Lars", "Thomas", "Emil", "Anders",

    // Wielka Brytania
    "James", "John", "William", "George", "Thomas", "Henry", "Edward", "Charles", "Michael", "David",

    // Irlandia
    "Seán", "Patrick", "Liam", "Conor", "Declan", "Brendan", "Ciarán", "Aidan", "Fionn", "Cathal",

    // Holandia
    "Daan", "Bram", "Luuk", "Sem", "Finn", "Lars", "Milan", "Ruben", "Jesse", "Thijs",

    // Belgia
    "Lucas", "Louis", "Arthur", "Léo", "Victor", "Adam", "Noah", "Liam", "Théo", "Tom",

    // Szwajcaria
    "Luca", "Elias", "Noah", "Leon", "Matteo", "Jonas", "Nico", "David", "Benjamin", "Julian",

    // Austria
    "Lukas", "Fabian", "Tobias", "Maximilian", "Julian", "Florian", "Paul", "Dominik", "Philipp", "David",

    // Czechy
    "Jan", "Jakub", "Tomáš", "Ondřej", "Martin", "Petr", "Lukáš", "Matěj", "Adam", "Vojtěch",

    // Słowacja
    "Peter", "Martin", "Michal", "Lukáš", "Matúš", "Marek", "Tomáš", "Andrej", "Jozef", "Filip",

    // Węgry
    "Bence", "Máté", "Levente", "Dávid", "Balázs", "Ádám", "László", "Gergő", "István", "Zoltán",

    // Rumunia
    "Andrei", "Alexandru", "Stefan", "Gabriel", "Mihai", "Cristian", "George", "Vlad", "Florin", "Radu",

    // Bułgaria
    "Ivan", "Georgi", "Dimitar", "Stefan", "Nikolay", "Petar", "Viktor", "Rumen", "Vasil", "Stoyan",

    // Grecja
    "Giorgos", "Dimitris", "Nikos", "Vasilis", "Kostas", "Panos", "Giannis", "Alexandros", "Michalis", "Christos",

    // Turcja
    "Mehmet", "Ahmet", "Mustafa", "Hüseyin", "Hasan", "İbrahim", "Ali", "Osman", "Murat", "Yusuf",

    // Serbia
    "Nikola", "Marko", "Miloš", "Stefan", "Aleksandar", "Luka", "Vladimir", "Filip", "Nenad", "Đorđe",

    // Chorwacja
    "Ivan", "Marko", "Ante", "Stjepan", "Luka", "Petar", "Matej", "Tomislav", "Josip", "Fran",

    // Bośnia i Hercegowina
    "Adnan", "Amir", "Haris", "Emir", "Dino", "Senad", "Faruk", "Tarik", "Nedim", "Alen",

    // Macedonia
    "Aleksandar", "Stefan", "Nikola", "Filip", "Kiril", "Marko", "Goran", "Viktor", "Martin", "Igor",

    // Albania
    "Arben", "Altin", "Dritan", "Erion", "Fatjon", "Gentian", "Ilir", "Kreshnik", "Leonard", "Valon",

    // Litwa
    "Jonas", "Dainius", "Mindaugas", "Tomas", "Mantas", "Giedrius", "Linas", "Andrius", "Paulius", "Arūnas",

    // Łotwa
    "Jānis", "Māris", "Andris", "Valdis", "Artūrs", "Rihards", "Roberts", "Edgars", "Kristaps", "Juris",

    // Estonia
    "Kristjan", "Martin", "Rasmus", "Mihkel", "Andres", "Juhan", "Toomas", "Tanel", "Siim", "Taavi",

    // Finlandia
    "Juhani", "Matti", "Mikko", "Juho", "Antti", "Heikki", "Kari", "Markus", "Timo", "Sami",

    // Islandia
    "Jón", "Sigurður", "Guðmundur", "Gunnar", "Björn", "Ólafur", "Einar", "Magnús", "Kristján", "Arnar",

    // USA
    "John", "Michael", "William", "David", "James", "Robert", "Daniel", "Joseph", "Charles", "Thomas",

    // Kanada
    "Liam", "Noah", "Jackson", "Lucas", "Logan", "Ethan", "James", "Mason", "Benjamin", "Oliver",

    // Australia
    "Jack", "William", "Oliver", "Noah", "Thomas", "James", "Liam", "Lucas", "Alexander", "Ethan",

    // Nowa Zelandia
    "Oliver", "Jack", "Noah", "James", "William", "Lucas", "Mason", "Liam", "Hunter", "Benjamin",

    // Brazylia
    "Gabriel", "Lucas", "Mateus", "Guilherme", "Pedro", "João", "Felipe", "Bruno", "Rafael", "Vitor",

    // Argentyna
    "Santiago", "Mateo", "Nicolás", "Joaquín", "Lucas", "Benjamín", "Martín", "Felipe", "Tomás", "Lautaro",

    // Meksyk
    "José", "Juan", "Luis", "Carlos", "Miguel", "Daniel", "Alejandro", "Jorge", "Eduardo", "Antonio",

    // Chile
    "Matías", "Agustín", "Benjamín", "Vicente", "Martín", "Lucas", "Cristóbal", "Joaquín", "Tomás", "Maximiliano",

    // Kolumbia
    "Santiago", "Mateo", "Sebastián", "Nicolás", "Samuel", "David", "Juan", "Daniel", "Julián", "Andrés",

    // Peru
    "José", "Luis", "Carlos", "Jorge", "Pedro", "Miguel", "Juan", "Víctor", "César", "Manuel",

    // Wenezuela
    "Miguel", "José", "Juan", "Luis", "Carlos", "Jorge", "Pedro", "Antonio", "Jesús", "Rafael",

    // Paragwaj
    "Santiago", "Juan", "Carlos", "Luis", "Miguel", "Daniel", "Jorge", "José", "Victor", "Fernando",

    // Urugwaj
    "Mateo", "Lucas", "Juan", "Felipe", "Santiago", "Martín", "Joaquín", "Nicolás", "Emiliano", "Thiago"
};


    public TextMeshProUGUI tombstoneText;

    void Start()
    {
        // Wywołanie metody, która zmienia tekst na nagrobku
        SetRandomZombieName();
    }

    void SetRandomZombieName()
    {
        // Losowanie indeksu z tablicy imion
        int randomIndex = Random.Range(0, zombieNames.Length);

        // Przypisanie losowego imienia do tekstu na nagrobku
        tombstoneText.text = "Here lies " + zombieNames[randomIndex];
    }
}
