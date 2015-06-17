# AutoFakes
Automatically initialize fake fields for unit tests

The follwing code uses FakeItEasy to create a fake object for every object marked with *FakeItAttribute* before each test run:

```
[TestFixture, AutoFakeItEasy]
public class UsingSimpleClassTests
{
    [FakeIt]
    private IDependency _fakeDependency;

    private IDependency _uninitializedDependency;

    [Test]
    public void FakesCreatedAutomatically()
    {
        Assert.That(_fakeDependency, Is.Not.Null);
    }

    [Test]
    public void FieldsWithoutAttributesAreNotInitialized()
    {
        Assert.That(_uninitializedDependency, Is.Null);
    }
}
```
