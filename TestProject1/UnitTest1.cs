using lab13;
using CollectionLibrary;
using CustomLibrary;

namespace lab13
{
    [TestClass]
    public class MyObserveCollectionTests
    {
        [TestMethod]
        public void Add_Item_ShouldInvokeCollectionCountChanged()
        {
            MyObserveCollection<Emoji> collection = new MyObserveCollection<Emoji>(2);
        }

        [TestMethod]
        public void Remove_Item_ShouldInvokeCollectionCountMinus()
        {
            MyObserveCollection<Emoji> collection = new MyObserveCollection<Emoji>(2);
            Emoji item = new Emoji();
            collection.Add(item);

            Assert.AreEqual(3, collection.Count);
        }

        [TestMethod]
        public void Add_Item_ShouldIncreaseCount()
        {
            MyObserveCollection<Emoji> collection = new MyObserveCollection<Emoji>(2);
            collection.Remove(collection.root.Data);

            //Assert.AreEqual(1, collection.Count);
        }


        [TestMethod]
        public void MyCollection_Remove_RemovesNodeIfExists()
        {
            // Arrange
            var collection = new MyCollection<Emoji>();
            var emoji1 = new Emoji("Test1", "Tag1", 1);
            var emoji2 = new Emoji("Test2", "Tag2", 2);
            collection.AddNode(emoji1);
            collection.AddNode(emoji2);

            // Act
            collection.Remove(emoji1);

            // Assert
            Assert.AreEqual(1, collection.Count);
            Assert.IsNull(collection.Find(emoji1));
        }

        [TestMethod]
        public void Remove_NodeNotInTree_ShouldReturnFalse()
        {
            // Arrange
            MyObserveCollection<Emoji> tree = new MyObserveCollection<Emoji>(3);
            Emoji nonExistentEmoji = new Emoji("NonExistent", "tag", 999);

            // Act
            bool result = tree.Remove(nonExistentEmoji);

            // Assert
            Assert.IsFalse(result, "Should return false when the node is not in the tree.");
        }

        [TestMethod]
        public void Remove_RootNodeWithoutChildren_ShouldReturnTrue()
        {
            // Arrange
            MyObserveCollection<Emoji> tree = new MyObserveCollection<Emoji>(3);
            Emoji rootEmoji = tree.root.Data;

            // Act
            bool result = tree.Remove(rootEmoji);

            // Assert
            Assert.IsTrue(result, "Should return true when the root node without children is removed.");
            //Assert.IsNull(tree.root, "The root should be null after removing the root node.");
        }

        [TestMethod]
        public void Remove_RootNodeWithOneChild_ShouldReturnTrue()
        {
            // Arrange
            MyObserveCollection<Emoji> tree = new MyObserveCollection<Emoji>(3);
            Emoji rootEmoji = tree.root.Data;
            Emoji childEmoji = new Emoji("Child", "tag", 1);
            tree.AddNode(childEmoji);

            // Act
            bool result = tree.Remove(rootEmoji);

            // Assert
            Assert.IsTrue(result, "Should return true when the root node with one child is removed.");
            //Assert.AreEqual(childEmoji, tree.root.Data, "The root should be replaced with its only child.");
        }

        [TestMethod]
        public void Remove_RootNodeWithTwoChildren_ShouldReturnTrue()
        {
            // Arrange
            MyObserveCollection<Emoji> tree = new MyObserveCollection<Emoji>(3);
            Emoji rootEmoji = tree.root.Data;
            Emoji leftChild = new Emoji("LeftChild", "tag", 1);
            Emoji rightChild = new Emoji("RightChild", "tag", 5);
            tree.AddNode(leftChild);
            tree.AddNode(rightChild);

            // Act
            bool result = tree.Remove(rootEmoji);

            // Assert
            Assert.IsTrue(result, "Should return true when the root node with two children is removed.");
            Assert.AreNotEqual(rootEmoji, tree.root.Data, "The root should be replaced.");
        }

        [TestMethod]
        public void Remove_NodeWithOneChild_ShouldReturnTrue()
        {
            // Arrange
            MyObserveCollection<Emoji> tree = new MyObserveCollection<Emoji>(3);
            Emoji parentEmoji = tree.root.Data;
            Emoji childEmoji = new Emoji("Child", "tag", 1);
            tree.AddNode(childEmoji);

            // Act
            bool result = tree.Remove(parentEmoji);

            // Assert
            Assert.IsTrue(result, "Should return true when a node with one child is removed.");
            //Assert.AreEqual(childEmoji, tree.root.Data, "The removed node should be replaced with its only child.");
        }

        [TestMethod]
        public void Remove_NodeWithTwoChildren_ShouldReturnTrue()
        {
            // Arrange
            MyObserveCollection<Emoji> tree = new MyObserveCollection<Emoji>(3);
            Emoji parentEmoji = tree.root.Data;
            Emoji leftChild = new Emoji("LeftChild", "tag", 1);
            Emoji rightChild = new Emoji("RightChild", "tag", 5);
            tree.AddNode(leftChild);
            tree.AddNode(rightChild);

            // Act
            bool result = tree.Remove(parentEmoji);

            // Assert
            Assert.IsTrue(result, "Should return true when a node with two children is removed.");
            Assert.AreNotEqual(parentEmoji, tree.root.Data, "The removed node should be replaced.");
        }

        [TestMethod]
        public void Remove_LeafNode_ShouldReturnTrue()
        {
            // Arrange
            MyObserveCollection<Emoji> tree = new MyObserveCollection<Emoji>(3);
            Emoji leafEmoji = new Emoji("Leaf", "tag", 1);
            tree.AddNode(leafEmoji);

            // Act
            bool result = tree.Remove(leafEmoji);

            // Assert
            Assert.IsTrue(result, "Should return true when a leaf node is removed.");
        }

        [TestClass]
        public class JournalTests
        {
            private Journal journal;

            [TestInitialize]
            public void Initialize()
            {
                journal = new Journal();
            }

            [TestMethod]
            public void WriteAdd_LogsAddEvent()
            {
                Journal journal = new Journal();
                MyObserveCollection<Emoji> emojis = new MyObserveCollection<Emoji>(1);

                var args = new CollectionHandlerEventArgs("1", "1");

                journal.WriteRecord(emojis, args);
                journal.PrintJournal();

                // Проверка, что событие добавления записано в журнал
                //Assert.IsTrue(journal.Contains("В коллекцию 1 был добавлен узел"));
            }

            [TestMethod]
            public void WriteDelete_LogsDeleteEvent()
            {
                Journal journal = new Journal();
                MyObserveCollection<Emoji> emojis = new MyObserveCollection<Emoji>(1); 
                var args = new CollectionHandlerEventArgs("1", "1");
                journal.WriteRecord(emojis, args);
                journal.PrintJournal();

                // Проверка, что событие удаления записано в журнал
                //Assert.IsTrue(journal.Contains("В коллекции 1 был удален узел"));
            }

           
            [TestMethod]
            public void PrintJournal_PrintsEmptyJournalMessage()
            {
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    journal.PrintJournal();

                    var expectedOutput = "Журнал пустой" + Environment.NewLine;
                    Assert.AreEqual(expectedOutput, sw.ToString());
                }
            }
        }


        [TestMethod]
        public void Remove_NonExistentItem_ShouldReturnFalse()
        {
            MyObserveCollection<Emoji> emojis = new MyObserveCollection<Emoji>(2);

            Emoji emoji = new Emoji();
            emoji.RandomInit();

            emojis[0] = emoji;

            Assert.AreEqual(true, emojis.Contains(emoji));
        }

        [TestMethod]
        public void Add_MultipleItems_ShouldMaintainOrder()
        {
            MyObserveCollection<Emoji> emojis = new MyObserveCollection<Emoji>(2);

            Assert.AreEqual(false, emojis[0].Name.Equals("a"));


        }
    }
}