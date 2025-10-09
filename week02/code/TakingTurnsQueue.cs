using System;
using System.Collections.Generic;

/// <summary>
/// This queue is circular. When people are added via AddPerson, they are added to the 
/// back of the queue (per FIFO rules). When GetNextPerson is called, the next person
/// in the queue is returned and then either re-enqueued or removed depending on their turns.
/// </summary>
public class TakingTurnsQueue
{
    private readonly Queue<Person> _people = new();

    public int Length => _people.Count;

    /// <summary>
    /// Add a new person with a given number of turns.
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        _people.Enqueue(new Person(name, turns));
    }

    /// <summary>
    /// Returns the next person in the queue.  
    /// If the person still has turns left, they are added back to the queue.
    /// People with turns <= 0 have infinite turns.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.Count == 0)
            throw new InvalidOperationException("No one in the queue.");

        Person person = _people.Dequeue();

        // Infinite turns
        if (person.Turns <= 0)
        {
            _people.Enqueue(person); // always goes back in
        }
        // Finite turns
        else
        {
            person.Turns--; // use up one turn

            // If the person has finite turns, decrement their turn and re-enqueue if they still have turns left
            if (person.Turns > 0)
            {
                _people.Enqueue(person); // still has turns left
            }
            // else: their turns are done, don't re-enqueue
        }

        return person;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _people)}]";
    }

}