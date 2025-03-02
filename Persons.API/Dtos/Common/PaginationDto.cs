namespace Persons.API.Dtos.Common
{
    public class PaginationDto<T> //Para que acepte cualquier valor 
    {
        public bool HasNextPage { get; set; }
        
        public bool HasPreviousPage { get; set; }
        //Esto me ayudara para llevar el control para que no tenga tanta data y se muestre en las paginas web 

        public int PageSize { get; set; }
        public int TotalItems { get; set; } // Total de los registro que hay en la cosulta
        public int TotalPages { get; set; }

        public T Items { get; set; }
    }
}
