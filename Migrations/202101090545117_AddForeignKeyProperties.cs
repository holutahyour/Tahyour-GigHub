namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyProperties : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Gigs", name: "Artist_Id", newName: "ArtistId");
            RenameColumn(table: "dbo.Gigs", name: "Genre_GenreId", newName: "GenreId");
            RenameIndex(table: "dbo.Gigs", name: "IX_Artist_Id", newName: "IX_ArtistId");
            RenameIndex(table: "dbo.Gigs", name: "IX_Genre_GenreId", newName: "IX_GenreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Gigs", name: "IX_GenreId", newName: "IX_Genre_GenreId");
            RenameIndex(table: "dbo.Gigs", name: "IX_ArtistId", newName: "IX_Artist_Id");
            RenameColumn(table: "dbo.Gigs", name: "GenreId", newName: "Genre_GenreId");
            RenameColumn(table: "dbo.Gigs", name: "ArtistId", newName: "Artist_Id");
        }
    }
}
