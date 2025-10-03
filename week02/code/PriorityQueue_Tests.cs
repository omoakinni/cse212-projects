using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ================== TESTS ==================

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several items with varying priorities; the last item has the highest priority.
    // Expected Result: Dequeue returns the value with the highest priority ("LastIsHighest"), proving the loop checks the last element.
    // Defect(s) Found: Original Dequeue loop used index < _queue.Count - 1 (off-by-one), skipping the last element.
    public void TestPriorityQueue_1_HighestPriorityFromLast()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("Low", 1);
        pq.Enqueue("Mid", 5);
        pq.Enqueue("LastIsHighest", 99); // last element has highest priority

        var result = pq.Dequeue();

        Assert.AreEqual("LastIsHighest", result);
    }

    [TestMethod]
    // Scenario: Multiple items share the highest priority; ensure FIFO among equals.
    // Expected Result: Dequeue returns the first inserted among the highest ("A").
    // Defect(s) Found: Original logic used >= which would overwrite the index and pick the last of equals.
    public void TestPriorityQueue_2_FIFOTieBreakOnHighest()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 10); // first highest
        pq.Enqueue("B", 10); // second highest (tie)
        pq.Enqueue("C", 5);

        var result = pq.Dequeue();

        Assert.AreEqual("A", result, "Should return the first item among the highest-priority ties (FIFO).");
    }

    [TestMethod]
    // Scenario: Dequeue on empty queue.
    // Expected Result: Throws InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None after fix; verifying requirement #4.
    public void TestPriorityQueue_3_EmptyQueueThrows()
    {
        var pq = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => pq.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Multiple dequeues with a mix of priorities and ties.
    // Expected Result: Dequeues in this exact order: X(50), A(10), B(10), C(9).
    // Defect(s) Found: Original code did not remove the item (RemoveAt missing) and tie-breaking was wrong.
    public void TestPriorityQueue_4_MultipleDequeuesOrder()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 10);
        pq.Enqueue("B", 10);
        pq.Enqueue("C", 9);
        pq.Enqueue("X", 50);

        Assert.AreEqual("X", pq.Dequeue(), "Highest should come out first.");
        Assert.AreEqual("A", pq.Dequeue(), "FIFO between A and B at pri=10.");
        Assert.AreEqual("B", pq.Dequeue(), "B should follow A for same priority.");
        Assert.AreEqual("C", pq.Dequeue(), "Remaining lowest.");
    }

    [TestMethod]
    // Scenario: Enqueue preserves insertion order (queue is not auto-sorted); verify ToString shape.
    // Expected Result: ToString shows items in enqueue order with "Value (Pri:Priority)" format.
    // Defect(s) Found: None; just validating Enqueue requirement #1 and ToString format.
    public void TestPriorityQueue_5_ToStringAndEnqueueOrder()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("First", 2);
        pq.Enqueue("Second", 2);
        pq.Enqueue("Third", 3);

        Assert.AreEqual("[First (Pri:2), Second (Pri:2), Third (Pri:3)]", pq.ToString());
    }
}