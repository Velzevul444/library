using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using books.Models;
using System;

namespace books.ViewModels;

public partial class BookWindowViewModel : ObservableObject
{
    public Action? CloseAction { get; set; }

    [ObservableProperty] private string title = "";
    [ObservableProperty] private string author = "";
    [ObservableProperty] private string genre = "";
    [ObservableProperty] private int year;
    [ObservableProperty] private int count;

    private readonly Book? editingBook;

    public BookWindowViewModel() { }

    public BookWindowViewModel(Book book)
    {
        editingBook = book;

        Title = book.Title;
        Author = book.Author;
        Genre = book.Genre;
        Year = book.Year;
        Count = book.Count;
    }

    [RelayCommand]
    private void Save()
    {
        if (editingBook == null)
        {
            var b = new Book
            {
                Title = Title,
                Author = Author,
                Genre = Genre,
                Year = Year,
                Count = Count
            };

            Db.Context.Books.Add(b);
        }
        else
        {
            editingBook.Title = Title;
            editingBook.Author = Author;
            editingBook.Genre = Genre;
            editingBook.Year = Year;
            editingBook.Count = Count;
        }

        Db.Context.SaveChanges();
        CloseAction?.Invoke();
    }

    [RelayCommand]
    private void Cancel()
    {
        CloseAction?.Invoke();
    }
}
