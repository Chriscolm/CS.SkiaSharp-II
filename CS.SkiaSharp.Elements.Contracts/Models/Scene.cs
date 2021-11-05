using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class Scene
    {
        private readonly ConcurrentBag<Element> _elements;
        private readonly ConcurrentDictionary<Type, Element> _replacables;

        public IEnumerable<Element> Elements => _elements.Concat(_replacables.Select(p => p.Value));
        public Settings Settings { get; set; }

        public Scene()
        {
            _elements = new ConcurrentBag<Element>();
            _replacables = new ConcurrentDictionary<Type, Element>();
        }

        public void Add(Element element)
        {
            _elements.Add(element);
        }

        public void Replace(Element element)
        {
            Element addFactory(Type t) { return element; }
            Element updateFactory(Type t, Element e)
            {
                return element;
            }
            var k = element.GetType();
            _replacables.AddOrUpdate(k, addFactory, updateFactory);
        }
    }
}
