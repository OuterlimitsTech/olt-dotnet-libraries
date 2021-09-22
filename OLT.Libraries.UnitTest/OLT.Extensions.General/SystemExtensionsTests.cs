using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Rules;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class SystemExtensionsTests
    {

        [Fact]
        public void ColorToHex()
        {
            Assert.Equal("#FF0000", System.Drawing.Color.Red.ToHex());
        }

        [Fact]
        public void ColorToRGB()
        {
            Assert.Equal("RGB(240,255,255)", System.Drawing.Color.Azure.ToRGB());
        }


        [Fact]
        public void DoesImplements()
        {
            var rule = new DoSomethingRuleDb();
            Assert.True(rule.GetType().Implements<IDoSomethingRuleDb>());
        }

        [Fact]
        public void DoesNotImplement()
        {
            var rule = new DoSomethingRuleOne();
            Assert.False(rule.GetType().Implements<IDoSomethingRuleDb>());
        }


        [Fact]
        public void ImplementException()
        {
            var rule = new DoSomethingRuleOne();
            Assert.Throws<InvalidOperationException>(() => rule.GetType().Implements<DoSomethingRuleDb>());
        }


        [Fact]
        public void DoesImplementsExtended()
        {
            var rule = new DoSomethingRuleDb();
            Assert.True(rule.GetType().Implements(typeof(IOltRuleAction<>)));
        }


        [Fact]
        public void DoesNoteImplementsExtended()
        {
            var rule = new DoSomethingRuleDb();
            Assert.False(rule.GetType().Implements(typeof(IOltAdapter<,>)));
        }


        [Fact]
        public void ImplementExceptionExtended()
        {
            var rule = new DoSomethingRuleOne();
            Assert.Throws<InvalidOperationException>(() => rule.GetType().Implements(typeof(OltAdapter<,>)));
        }

    }
}