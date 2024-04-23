using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class step1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "QuizEntityQuizItemEntity",
                columns: new[] { "ItemsId", "QuizzesId" },
                values: new object[] { 4, 1 });

            migrationBuilder.InsertData(
                table: "QuizItems",
                columns: new[] { "Id", "CorrectAnswer", "Question" },
                values: new object[,]
                {
                    { 5, "5", "2x=10" },
                    { 6, "6", "4y=24" },
                    { 7, "5", "5x=25" },
                    { 8, "2", "8x=16" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[] { 1, "jedynka@onet.pl", "12345" });

            migrationBuilder.InsertData(
                table: "QuizEntityQuizItemEntity",
                columns: new[] { "ItemsId", "QuizzesId" },
                values: new object[,]
                {
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 }
                });

            migrationBuilder.InsertData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                columns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                values: new object[,]
                {
                    { 1, 8 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 3, 7 },
                    { 4, 8 },
                    { 6, 5 },
                    { 7, 7 },
                    { 8, 5 },
                    { 8, 6 },
                    { 8, 8 },
                    { 9, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuizId",
                table: "UserAnswers",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Quizzes_QuizId",
                table: "UserAnswers",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Users_UserId",
                table: "UserAnswers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Quizzes_QuizId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Users_UserId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_QuizId",
                table: "UserAnswers");

            migrationBuilder.DeleteData(
                table: "QuizEntityQuizItemEntity",
                keyColumns: new[] { "ItemsId", "QuizzesId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "QuizEntityQuizItemEntity",
                keyColumns: new[] { "ItemsId", "QuizzesId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "QuizEntityQuizItemEntity",
                keyColumns: new[] { "ItemsId", "QuizzesId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "QuizEntityQuizItemEntity",
                keyColumns: new[] { "ItemsId", "QuizzesId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "QuizEntityQuizItemEntity",
                keyColumns: new[] { "ItemsId", "QuizzesId" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 9, 6 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuizItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "QuizItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "QuizItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "QuizItems",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
