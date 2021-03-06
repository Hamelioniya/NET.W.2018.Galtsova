﻿using System;
using System.Collections.Generic;

namespace DataStructure
{
    /// <summary>
    /// Represents a binary search tree.
    /// </summary>
    /// <typeparam name="T">Type of the element of the node.</typeparam>
    public class BinarySearchTree<T>
    {
        #region Private fields

        private BinaryTreeNode<T> _root;
        private int _count;
        private Comparison<T> _comparator;

        #endregion !Private fields.

        #region Public constructors

        /// <summary>
        /// Initializes an instance of the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        public BinarySearchTree() : this(GetComparator())
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="BinarySearchTree{T}"/> with the passed comparator.
        /// </summary>
        /// <param name="comparator">The comparator to compare nodes of binary tree.</param>
        public BinarySearchTree(Comparison<T> comparator)
        {
            Comparator = comparator;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="BinarySearchTree{T}"/> with the passed collection.
        /// </summary>
        /// <param name="collection">The collection to add to the tree.</param>
        public BinarySearchTree(IEnumerable<T> collection) : this(collection, GetComparator())
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="BinarySearchTree{T}"/> with passed collection and comparator.
        /// </summary>
        /// <param name="collection">The collection to add to the tree.</param>
        /// <param name="comparator">The comparator to compare nodes of binary tree.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="collection"/> equal to null.
        /// </exception>
        public BinarySearchTree(IEnumerable<T> collection, Comparison<T> comparator)
        {
            if (ReferenceEquals(collection, null))
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Comparator = comparator;

            foreach (T item in collection)
            {
                this.Add(item);
            }
        }

        #endregion !Public constructors.

        #region Public properties

        /// <summary>
        /// The count of binary tree nodes.
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        #endregion !Public properties.

        #region Private properties

        private Comparison<T> Comparator
        {
            set
            {
                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException(nameof(Comparator));
                }

                _comparator = value;
            }
        }

        #endregion !Private properties.

        #region Public methods

        /// <summary>
        /// Adds the element to the binary search tree.
        /// </summary>
        /// <param name="value">The element to add.</param>
        public void Add(T value)
        {
            if (ReferenceEquals(_root, null))
            {
                _root = new BinaryTreeNode<T>(value);
            }
            else
            {
                AddTo(_root, value);
            }

            _count++;
        }

        /// <summary>
        /// Searches the <paramref name="value"/> in the binary search tree.
        /// </summary>
        /// <param name="value">The element to find.</param>
        /// <returns>true if the <paramref name="value"/> is contained in the binary search tree, false otherwise.</returns>
        public bool Contains(T value)
        {
            return Find(value, _root) != null;
        }

        /// <summary>
        /// Removes the first node with the <paramref name="value"/> from the binary search tree.
        /// </summary>
        /// <param name="value">The value of the node to remove.</param>
        public void Remove(T value)
        {
            if (Contains(value))
            {
                _root = Remove(_root, value);
            }
        }

        /// <summary>
        /// Clears the binary search tree.
        /// </summary>
        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        /// <summary>
        /// Preorder traversal.
        /// </summary>
        /// <returns>The element of the sequence generated by the traversal.</returns>
        public IEnumerable<T> PreOrderTraversal()
        {
            return PreOrderTraversal(_root);
        }

        /// <summary>
        /// Postorder traversal.
        /// </summary>
        /// <returns>The element of the sequence generated by the traversal.</returns>
        public IEnumerable<T> PostOrderTraversal()
        {
            return PostOrderTraversal(_root);
        }

        /// <summary>
        /// Inorder traversal.
        /// </summary>
        /// <returns>The element of the sequence generated by the traversal.</returns>
        public IEnumerable<T> InOrderTraversal()
        {
            return InOrderTraversal(_root);
        }

        /// <summary>
        /// Preorder traversal.
        /// </summary>
        /// <param name="root">The root of the traversal.</param>
        /// <returns>The element of the sequence generated by the traversal.</returns>
        public IEnumerable<T> PreOrderTraversal(BinaryTreeNode<T> root)
        {
            if (ReferenceEquals(root, null))
            {
                yield break;
            }

            yield return root.Value;

            if (!ReferenceEquals(root.Left, null))
            {
                foreach (var item in PreOrderTraversal(root.Left))
                {
                    yield return item;
                }
            }

            if (!ReferenceEquals(root.Right, null))
            {
                foreach (var item in PreOrderTraversal(root.Right))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Postorder traversal.
        /// </summary>
        /// <param name="root">The root of the traversal.</param>
        /// <returns>The element of the sequence generated by the traversal.</returns>
        public IEnumerable<T> PostOrderTraversal(BinaryTreeNode<T> root)
        {
            if (ReferenceEquals(root, null))
            {
                yield break;
            }

            if (!ReferenceEquals(root.Left, null))
            {
                foreach (var item in PostOrderTraversal(root.Left))
                {
                    yield return item;
                }
            }

            if (!ReferenceEquals(root.Right, null))
            {
                foreach (var item in PostOrderTraversal(root.Right))
                {
                    yield return item;
                }
            }

            yield return root.Value;
        }

        /// <summary>
        /// Inorder traversal.
        /// </summary>
        /// <param name="root">The root of the traversal.</param>
        /// <returns>The element of the sequence generated by the traversal.</returns>
        public IEnumerable<T> InOrderTraversal(BinaryTreeNode<T> root)
        {
            if (ReferenceEquals(root, null))
            {
                yield break;
            }

            if (!ReferenceEquals(root.Left, null))
            {
                foreach (var item in InOrderTraversal(root.Left))
                {
                    yield return item;
                }
            }

            yield return root.Value;

            if (!ReferenceEquals(root.Right, null))
            {
                foreach (var item in InOrderTraversal(root.Right))
                {
                    yield return item;
                }
            }
        }

        #endregion !Public methods.

        #region Private methods

        private static Comparison<T> GetComparator()
        {
            return Comparer<T>.Default.Compare;
        }

        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            if (_comparator(value, node.Value) < 0)
            {
                if (ReferenceEquals(node.Left, null))
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Left, value);
                }
            }
            else
            {
                if (ReferenceEquals(node.Right, null))
                { 
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
        }

        private BinaryTreeNode<T> Find(T value, BinaryTreeNode<T> node)
        {
            if (ReferenceEquals(node, null))
            {
                return null;
            }

            if (_comparator(node.Value, value) == 0)
            {
                return node;
            }

            if (_comparator(node.Value, value) > 0)
            {
                return Find(value, node.Left);
            }

            return Find(value, node.Right);
        }

        private BinaryTreeNode<T> Remove(BinaryTreeNode<T> node, T value)
        {
            if (node == null)
            {
                return node;
            }

            if (_comparator(value, node.Value) < 0)
            {
                node.Left = Remove(node.Left, value);
            }
            else
            {
                if (_comparator(value, node.Value) > 0)
                {
                    node.Right = Remove(node.Right, value);
                }
                else
                {
                    if (!ReferenceEquals(node.Left, null) && !ReferenceEquals(node.Right, null))
                    {
                        node.Value = MinimumValue(node.Right).Value;
                        node.Right = Remove(node.Right, node.Value);
                    }
                    else
                    {
                        if (!ReferenceEquals(node.Left, null))
                        {
                            node = node.Left;
                        }
                        else
                        {
                            node = node.Right;
                        }
                    }
                }
            }

            return node;
        }

        private BinaryTreeNode<T> MinimumValue(BinaryTreeNode<T> node)
        {
            if (ReferenceEquals(node.Left, null))
            {
                return node;
            }

            return MinimumValue(node.Left);
        }

        #endregion !Private methods.
    }
}
