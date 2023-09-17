using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DJM.EventManager.Tests
{
    [TestFixture]
    internal sealed class EventManagerTests
    {
        private EventManagerService _eventManagerService;
        
        [SetUp]
        public void SetUp()
        {
            _eventManagerService = new EventManagerService();
        }


        [Test]
        public void Subscribe_EmptyStructEvent_NullListener_ShouldLogError()
        {
            // arrange
            var expectedErrorMessage = ErrorMessages.NullListenerSubscribed(typeof(EmptyStructEvent));
            
            // act
            _eventManagerService.Subscribe<EmptyStructEvent>(null);
            
            // assert
            LogAssert.Expect(LogType.Error, expectedErrorMessage);
        }
        [Test]
        public void Subscribe_Vector2Event_NullListener_ShouldLogError()
        {
            // arrange
            var expectedErrorMessage = ErrorMessages.NullListenerSubscribed(typeof(Vector2));
            
            // act
            _eventManagerService.Subscribe<Vector2>(null);
            
            // assert
            LogAssert.Expect(LogType.Error, expectedErrorMessage);
        }
        
        
        [Test]
        public void SubscribeAndTrigger_EmptyStructEvent_ValidListener_ShouldCallListener()
        {
            // arrange
            var called = false;
            Action<EmptyStructEvent> listener = _ => called = true;
            
            // act
            _eventManagerService.Subscribe(listener);
            _eventManagerService.TriggerEvent(new EmptyStructEvent());
    
            // assert
            Assert.AreEqual(true, called);
        }
        [Test]
        public void SubscribeAndTrigger_Vector2Event_ValidListener_ShouldCallListener()
        {
            // arrange
            var callbackValue = Vector2.zero;
            Action<Vector2> listener = (vec2) => callbackValue = vec2;
            
            // act
            _eventManagerService.Subscribe(listener);
            _eventManagerService.TriggerEvent(Vector2.one);
    
            // assert
            Assert.AreEqual(Vector2.one, callbackValue);
        }
        
        
        [Test]
        public void Unsubscribe_EmptyStructEvent_ValidListener_ShouldNotCallListener()
        {
            // arrange
            var called = false;
            Action<EmptyStructEvent> listener = _ => called = true;
            
            // act
            _eventManagerService.Subscribe(listener);
            _eventManagerService.Unsubscribe(listener);
            _eventManagerService.TriggerEvent(new EmptyStructEvent());
    
            // assert
            Assert.AreEqual(false, called);
        }
        [Test]
        public void Unsubscribe_Vector2Event_ValidListener_ShouldNotCallListener()
        {
            // arrange
            var callbackValue = Vector2.zero;
            Action<Vector2> listener = (vec2) => callbackValue = vec2;
            
            // act
            _eventManagerService.Subscribe(listener);
            _eventManagerService.Unsubscribe(listener);
            _eventManagerService.TriggerEvent(Vector2.one);
    
            // assert
            Assert.AreEqual(Vector2.zero, callbackValue);
        }
        
        
        [Test]
        public void UnsubscribeWithoutSubscribing_EmptyStructEvent_ValidListener_ShouldNotThrow()
        {
            // arrange
            Action<EmptyStructEvent> listener = _ => { };
            
            // act
            _eventManagerService.Unsubscribe(listener);
    
            // assert
            Assert.DoesNotThrow(() => _eventManagerService.Unsubscribe(listener));
        }
        [Test]
        public void UnsubscribeWithoutSubscribing_Vector2Event_ValidListener_ShouldNotThrow()
        {
            // arrange
            Action<Vector2> listener = (vec2) => _ = vec2;
            
            // act
            _eventManagerService.Unsubscribe(listener);
    
            // assert
            Assert.DoesNotThrow(() => _eventManagerService.Unsubscribe(listener));
        }
        
        
        [Test]
        public void TriggerEvent_EmptyStructEvent_NoSubscribers_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() => _eventManagerService.TriggerEvent(new EmptyStructEvent()));
        }
        [Test]
        public void TriggerEvent_Vector2Event_NoSubscribers_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() => _eventManagerService.TriggerEvent(Vector2.one));
        }
        
        
        [Test]
        public void Reset_EmptyStructEvent_NoSubscribers_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() => _eventManagerService.ClearAllEvents());
        }
        
        
        [Test]
        public void Reset_VariousTypeSubscribers_ShouldRemoveAll()
        {
            // arrange
            var vec2CallbackValue = Vector2.zero;
            var emptyStructCalled = false;
            var valueStructCallBackValue = 0;
            
            Action<Vector2> vec2Listener = (vec2) => vec2CallbackValue = vec2;
            Action<EmptyStructEvent> emptyStructEventListener = _ => emptyStructCalled = true;
            Action<ValueStructEvent> valueStructEventListener = (valueStruct) => valueStructCallBackValue = valueStruct.Value;
            
            _eventManagerService.Subscribe(vec2Listener);
            _eventManagerService.Subscribe(emptyStructEventListener);
            _eventManagerService.Subscribe(valueStructEventListener);
            
            // act
            _eventManagerService.ClearAllEvents();
            _eventManagerService.TriggerEvent(Vector2.one);
            _eventManagerService.TriggerEvent(new EmptyStructEvent());
            _eventManagerService.TriggerEvent(new ValueStructEvent(1));
            
            // assert
            Assert.AreEqual(Vector2.zero, vec2CallbackValue);
            Assert.AreEqual(false, emptyStructCalled);
            Assert.AreEqual(0, valueStructCallBackValue);
        }
        
        
        [Test]
        public void RemoveEvent_EmptyStructEvent_NoSubscribers_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() => _eventManagerService.ClearEvent<EmptyStructEvent>());
        }
        [Test]
        public void RemoveEvent_Vector2Event_NoSubscribers_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() => _eventManagerService.TriggerEvent(Vector2.one));
        }
        
        
        [Test]
        public void RemoveEvent_EmptyStructEvent_VariousSubscribers_ShouldRemoveOnlyThatType()
        {
            // arrange
            var vec2CallbackValue = Vector2.zero;
            var emptyStructCalled = false;
            var valueStructCallBackValue = 0;
            
            Action<Vector2> vec2Listener = (vec2) => vec2CallbackValue = vec2;
            Action<EmptyStructEvent> emptyStructEventListener = _ => emptyStructCalled = true;
            Action<ValueStructEvent> valueStructEventListener = (valueStruct) => valueStructCallBackValue = valueStruct.Value;
            
            _eventManagerService.Subscribe(vec2Listener);
            _eventManagerService.Subscribe(emptyStructEventListener);
            _eventManagerService.Subscribe(valueStructEventListener);
            
            // act
            _eventManagerService.ClearEvent<EmptyStructEvent>();
            _eventManagerService.TriggerEvent(Vector2.one);
            _eventManagerService.TriggerEvent(new EmptyStructEvent());
            _eventManagerService.TriggerEvent(new ValueStructEvent(1));
            
            // assert
            Assert.AreEqual(Vector2.one, vec2CallbackValue);
            Assert.AreEqual(false, emptyStructCalled);
            Assert.AreEqual(1, valueStructCallBackValue);
        }
        [Test]
        public void RemoveEvent_Vector2Event_VariousSubscribers_ShouldRemoveOnlyThatType()
        {
            // arrange
            var vec2CallbackValue = Vector2.zero;
            var emptyStructCalled = false;
            var valueStructCallBackValue = 0;
            
            Action<Vector2> vec2Listener = (vec2) => vec2CallbackValue = vec2;
            Action<EmptyStructEvent> emptyStructEventListener = _ => emptyStructCalled = true;
            Action<ValueStructEvent> valueStructEventListener = (valueStruct) => valueStructCallBackValue = valueStruct.Value;
            
            _eventManagerService.Subscribe(vec2Listener);
            _eventManagerService.Subscribe(emptyStructEventListener);
            _eventManagerService.Subscribe(valueStructEventListener);
            
            // act
            _eventManagerService.ClearEvent<Vector2>();
            _eventManagerService.TriggerEvent(Vector2.one);
            _eventManagerService.TriggerEvent(new EmptyStructEvent());
            _eventManagerService.TriggerEvent(new ValueStructEvent(1));
            
            // assert
            Assert.AreEqual(Vector2.zero, vec2CallbackValue);
            Assert.AreEqual(true, emptyStructCalled);
            Assert.AreEqual(1, valueStructCallBackValue);
        }
    }
    
    internal readonly struct EmptyStructEvent { }
    internal readonly struct ValueStructEvent
    {
        public readonly int Value;
        public ValueStructEvent(int value)
        {
            Value = value;
        }
    }
}