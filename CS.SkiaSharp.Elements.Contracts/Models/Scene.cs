using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CS.SkiaSharpExample.Elements.Contracts.Models
{
    public class Scene
    {
        private readonly ConcurrentBag<Element> _elements;
        public IEnumerable<Element> Elements => _elements;
        public GeneralSettings Settings { get; set; }

        public Scene()
        {
            _elements = new ConcurrentBag<Element>();            
        }

        public void Add(Element element)
        {
            _elements.Add(element);
        } 
        
        public void Clear()
        {
            _elements.Clear();
        }
    }
}
