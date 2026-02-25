using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using books.Data;
using books.Models;

namespace books.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly BooksDbContext _context = new();

    public ObservableCollection<Book> Books { get; set; } = new();

    [ObservableProperty]
    private Book? selectedBook;

    [ObservableProperty]
    private string? searchText;

    public MainWindowViewModel()
    {
        Load();
    }

    private void Load()
    {
        Books = new ObservableCollection<Book>(_context.Books.ToList());
    }

    partial void OnSearchTextChanged(string? value)
    {
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        var q = _context.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var text = SearchText.ToLower();
            q = q.Where(b =>
                b.Title.ToLower().Contains(text) ||
                b.Author.ToLower().Contains(text) ||
                b.Genre.ToLower().Contains(text)
            );
        }

        Books = new ObservableCollection<Book>(q.ToList());
        OnPropertyChanged(nameof(Books));
    }

    [RelayCommand]
    private void Add()
    {
        var book = new Book
        {
            Title = "Новая книга",
            Author = "Автор",
            Genre = "Жанр",
            Year = 2000,
            Count = 1
        };

        _context.Books.Add(book);
        _context.SaveChanges();
        Load();
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedBook == null) return;

        SelectedBook.Title += " (изм)";
        _context.SaveChanges();
        Load();
    }

    [RelayCommand]
    private void Delete()
    {
        if (SelectedBook == null) return;

        _context.Books.Remove(SelectedBook);
        _context.SaveChanges();
        Load();
    }
}
