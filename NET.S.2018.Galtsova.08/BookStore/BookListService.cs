﻿using System;
using System.Collections.Generic;
using BookStore.Exceptions;

namespace BookStore
{
    /// <summary>
    /// Represents a work with the list of books.
    /// </summary>
    public class BookListService
    {
        #region Private fields

        private List<Book> _books;
        private IBookListStorage _bookListStorage;

        #endregion !Private fields.

        #region Public constructors

        /// <summary>
        /// Initializes an instance of the <see cref="Book"/> with the passed book list storage.
        /// </summary>
        /// <param name="bookListStorage">An instance of an interface.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="bookListStorage"/> equal to null.
        /// </exception>
        public BookListService(IBookListStorage bookListStorage)
        {
            if (ReferenceEquals(bookListStorage, null))
            {
                throw new ArgumentNullException(nameof(bookListStorage));
            }

            Books = new List<Book>();
            _bookListStorage = bookListStorage;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Book"/> with passed book list storage and list of books.
        /// </summary>
        /// <param name="books">A list of books.</param>
        /// <param name="bookListStorage">An instance of an interface.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="books"/> or/and <paramref name="bookListStorage"/> equal to null.
        /// </exception>
        public BookListService(List<Book> books, IBookListStorage bookListStorage)
        {
            if (ReferenceEquals(bookListStorage, null))
            {
                throw new ArgumentNullException(nameof(bookListStorage));
            }

            if (ReferenceEquals(books, null))
            {
                throw new ArgumentNullException(nameof(books));
            }

            Books = books;
            _bookListStorage = bookListStorage;
        }

        #endregion !Public constructors.

        #region Public properties

        /// <summary>
        /// List of books.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="cref"/> equal to null.
        /// </exception>
        public List<Book> Books
        {
            get
            {
                return _books;
            }

            set
            {
                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException(nameof(Books));
                }

                _books = value;
            }
        }

        #endregion ! Public properties.

        #region Public methods

        /// <summary>
        /// Adds a <paramref name="book"/> to the list of books.
        /// </summary>
        /// <param name="book">A book.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="book"/> equal to null.
        /// </exception>
        /// <exception cref="BookAlreadyExistsException">
        /// Thrown when <paramref name="book"/> already exists in the list of books.
        /// </exception>
        public void AddBook(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (Books.Contains(book))
            {
                throw new BookAlreadyExistsException("This book already exists.");
            }

            Books.Add(book);
        }

        /// <summary>
        /// Removes a <paramref name="book"/> from the list of books.
        /// </summary>
        /// <param name="book">A book.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="book"/> equal to null.
        /// </exception>
        /// <exception cref="BookNotFoundException">
        /// Thrown when <paramref name="book"/> not found in the list of books.
        /// </exception>
        public void RemoveBook(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (!Books.Contains(book))
            {
                throw new BookNotFoundException("This book not found.");
            }

            Books.Remove(book);
        }

        /// <summary>
        /// Finds book by the <paramref name="isbn"/> in the list of books.
        /// </summary>
        /// <param name="isbn">An ISBN.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="isbn"/> less than 0.
        /// </exception>
        /// <returns>A found book.</returns>
        public Book FindBookByISBN(int isbn)
        {
            if (isbn < 0)
            {
                throw new ArgumentOutOfRangeException("ISBN must be greater than 0.", nameof(isbn));
            }

            foreach (Book book in Books)
            {
                if (book.ISBN == isbn)
                {
                    return book;
                }
            }

            return null;
        }

        #endregion !Public methods.

        #region Sort public methods

        /// <summary>
        /// Sorts a list of books by the ISBN.
        /// </summary>
        public void SortByISBN()
        {
            Books.Sort(Comparator.CompareByISBN);
        }

        /// <summary>
        /// Sorts a list of books by the author.
        /// </summary>
        public void SortByAuthor()
        {
            Books.Sort(Comparator.CompareByAuthor);
        }

        /// <summary>
        /// Sorts a list of books by the name.
        /// </summary>
        public void SortByName()
        {
            Books.Sort(Comparator.CompareByName);
        }

        /// <summary>
        /// Sorts a list of books by the publishing house.
        /// </summary>
        public void SortByPublishingHouse()
        {
            Books.Sort(Comparator.CompareByPublishingHouse);
        }

        /// <summary>
        /// Sorts a list of books by the publishing year.
        /// </summary>
        public void SortByPublishingYear()
        {
            Books.Sort(Comparator.CompareByPublishingYear);
        }

        /// <summary>
        /// Sorts a list of books by the number of pages.
        /// </summary>
        public void SortByNumOfPages()
        {
            Books.Sort(Comparator.CompareByNumOfPages);
        }

        /// <summary>
        /// Sorts a list of books by the price.
        /// </summary>
        public void SortByPrice()
        {
            Books.Sort(Comparator.CompareByPrice);
        }

        /// <summary>
        /// Sorts a list of books by <paramref name="comparator"/>.
        /// </summary>
        /// <param name="comparator">A comparator for sort.</param>
        public void Sort(IComparer<Book> comparator)
        {
            Books.Sort(comparator);
        }

        #endregion !Sort public methods.

        #region Work with file public methods

        /// <summary>
        /// Loads a list of books to the file with the path <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">A file path.</param>
        public void LoadBooksListFromFile(string filePath)
        {
            Books.Clear();
            Books = _bookListStorage.GetListOfBooks(filePath);
        }

        /// <summary>
        /// Saves a list of books to the file with the path <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">A file path.</param>
        public void SaveBooksListToFile(string filePath)
        {
            _bookListStorage.SaveListOfBooks(filePath, Books);
        }

        #endregion !Work with file public methods.
    }
}