using lab13;
using CollectionLibrary;
using CustomLibrary;

namespace TestProject1
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
           
        }

        [TestMethod]
        public void Remove_Item_ShouldDecreaseCount()
        {
            
        }

        [TestMethod]
        public void Remove_NonExistentItem_ShouldReturnFalse()
        {
            
        }

        [TestMethod]
        public void Add_MultipleItems_ShouldMaintainOrder()
        {
            
        }
    }
}