﻿namespace PuSocialNetwork.Data.Migrations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;

    [DbContext(typeof(SocialNetworkDbContext))]
    [Migration("20190314140631_UserPollTable")]
    partial class UserPollTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostId");

                    b.Property<string>("Text");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Play2048Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FinalScore");

                    b.Property<int>("MaxNumber");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Play2048Scores");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Poll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Question");

                    b.HasKey("Id");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.PollAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PollId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.ToTable("PollAnswer");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<bool>("IsYoutubeVideo");

                    b.Property<DateTime>("PostDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("BirthPlace");

                    b.Property<int>("CourseId");

                    b.Property<string>("Egn")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FacultyNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<byte[]>("ProfileImage");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("Egn")
                        .IsUnique();

                    b.HasIndex("FacultyNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.UserPoll", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("PollId");

                    b.HasKey("UserId", "PollId");

                    b.HasIndex("PollId");

                    b.ToTable("UserPoll");
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Article", b =>
                {
                    b.HasOne("PuSocialNetwork.Data.Models.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Comment", b =>
                {
                    b.HasOne("PuSocialNetwork.Data.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PuSocialNetwork.Data.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Like", b =>
                {
                    b.HasOne("PuSocialNetwork.Data.Models.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PuSocialNetwork.Data.Models.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Play2048Score", b =>
                {
                    b.HasOne("PuSocialNetwork.Data.Models.User", "User")
                        .WithMany("Play2048Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.PollAnswer", b =>
                {
                    b.HasOne("PuSocialNetwork.Data.Models.Poll", "Poll")
                        .WithMany("Answers")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.Post", b =>
                {
                    b.HasOne("PuSocialNetwork.Data.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.User", b =>
                {
                    b.HasOne("PuSocialNetwork.Data.Models.Course", "Course")
                        .WithMany("Users")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PuSocialNetwork.Data.Models.UserPoll", b =>
                {
                    b.HasOne("PuSocialNetwork.Data.Models.Poll", "Poll")
                        .WithMany("Users")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PuSocialNetwork.Data.Models.User", "User")
                        .WithMany("Polls")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
