﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoApi;

public partial class ToDoDBContext : DbContext
{
    public ToDoDBContext()
    {
    }

    public ToDoDBContext(DbContextOptions<ToDoDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string url ="server=bnyu2rpupoothabum3gz-mysql.services.clever-cloud.com;database=bnyu2rpupoothabum3gz;user=udbq9sebjleqnttw;password=AlfRu4efirNKxgJPzeD1";
          //  string connectionString = "mysql://udbq9sebjleqnttw:AlfRu4efirNKxgJPzeD1@bnyu2rpupoothabum3gz-mysql.services.clever-cloud.com:3306/bnyu2rpupoothabum3gz";
            optionsBuilder.UseMySql(url, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));
        }
    }
    //   => optionsBuilder.UseMySql("name=ToDoDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Items");

            entity.Property(e => e.Name).HasMaxLength(100);
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
