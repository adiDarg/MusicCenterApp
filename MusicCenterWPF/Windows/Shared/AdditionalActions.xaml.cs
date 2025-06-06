using MusicCenterWPF.Windows.Admin;
using MusicCenterWPF.Windows.Registree;
using MusicCenterWPF.Windows.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utility;

namespace MusicCenterWPF.Windows.Shared
{
    /// <summary>
    /// Interaction logic for AdditionalActions.xaml
    /// </summary>
    public partial class AdditionalActions : Window
    {
        private Style? buttonStyle;
        private Style? borderStyle;
        public AdditionalActions()
        {
            buttonStyle = this.FindResource("StyledButton") as Style;
            borderStyle = this.FindResource("CellBorderStyle") as Style;
            InitializeComponent();
            this.Loaded += (s, e) => {
                switch (SessionManager.Type) {
                    case "User":
                        {
                            UserAdditionalActions();
                            break;
                        }
                    case "Registree":
                        {
                            RegistreeAdditionalActions();
                            break;
                        }
                    case "Teacher":
                    case "Instructor":
                        {
                            TeacherAdditionalActions();
                            break;
                        }
                    case "Admin":
                        {
                            AdminAdditionalActions();
                            break;
                        }
                }
            };
        }
        private void UserAdditionalActions() 
        { 
            //Enter Validation Key
            Button enterVKbutton = new Button
            {
                Content = "Enter Validation Key",
                Style = buttonStyle
            };
            enterVKbutton.Click += (s, e) =>
            {
                this.Visibility = Visibility.Hidden;
                new EnterValidationKey().Show();
            };
            
            //Send Validation Key
            Button sendVKbutton = new Button { 
                Content = "Send Validation Key",
                Style = buttonStyle
            };
            sendVKbutton.Click += async (s, e) =>
            {
                bool success = await EmailUtils.SendValidationKeyEmail(SessionManager.UserID);
                MessageBox.Show(success ? "Email sent." : "Email failed to send. Make sure your email address is valid.",
                    success ? "Success" : "Error",
                    MessageBoxButton.OK,
                    success ? MessageBoxImage.Information: MessageBoxImage.Error);
            };
            
            //Set the grid
            actionsGrid.RowDefinitions.Add(new RowDefinition());
            actionsGrid.RowDefinitions.Add(new RowDefinition());
            
            Grid.SetRow(enterVKbutton, 0);
            Grid.SetRow(sendVKbutton, 1);
            
            actionsGrid.Children.Add(enterVKbutton);
            actionsGrid.Children.Add(sendVKbutton);
        }
        private void RegistreeAdditionalActions() 
        {
            //View Teachers
            Button viewTeachersButton = new Button {
                Content = "View Teachers",
                Style = buttonStyle
            };
            viewTeachersButton.Click += (s, e) =>
            {
                this.Visibility = Visibility.Hidden;
                new ViewTeachers().Show();
            };
            
            //Only 1 child so no need for setup
            actionsGrid.Children.Add(viewTeachersButton);
        }
        private void TeacherAdditionalActions()
        {
            //Add Instrument
            Button addInstrumentButton = new Button { 
                Content = "Add Instrument",
                Style = buttonStyle
            };
            addInstrumentButton.Click += (s, e) =>
            {
                this.Visibility = Visibility.Hidden;
                new AddNewInstrument().Show();
            };
            //Only 1 child so no need for setup
            actionsGrid.Children.Add(addInstrumentButton);
        }
        private void AdminAdditionalActions()
        {
            //Lots of actions, so keep DRY with List
            List<Button> actions = new List<Button>();
            //Create Group
            Button createGroupButton = new Button { Content = "Create Group" };
            createGroupButton.Click += (s, e) => { new CreateGroup().Show(); };
            actions.Add(createGroupButton);

            //Update Instructor To Group
            Button updateInstructorToGroupButton = new Button { Content = "Update Instructor Of Group" };
            updateInstructorToGroupButton.Click += (s, e) => { new UpdateInstructorOfGroup().Show(); };
            actions.Add(updateInstructorToGroupButton);

            //Add Registree To Group
            Button addRegistreeToGroupButton = new Button { Content = "Add Registree To Group" };
            addRegistreeToGroupButton.Click += (s, e) => { new AddRegistreeToGroup().Show(); };
            actions.Add(addRegistreeToGroupButton);

            //Remove Registree From Group
            Button removeRegistreeFromGroupButton = new Button { Content = "Remove Registree From Group" };
            removeRegistreeFromGroupButton.Click += (s, e) => { new RemoveRegistreeFromGroup().Show(); };
            actions.Add(removeRegistreeFromGroupButton);

            //Display Members
            Button displayMembersButton = new Button { Content = "Display Members" };
            displayMembersButton.Click += (s, e) => { new DisplayMembers().Show(); };
            actions.Add(displayMembersButton);

            //Promote & Demote
            Button promoteAndDemoteButton = new Button { Content = "Promote Or Demote" };
            promoteAndDemoteButton.Click += (s, e) => { new PromoteAndDemote().Show(); };
            actions.Add(promoteAndDemoteButton);

            //Schedule Event
            Button scheduleEvent = new Button { Content = "Schedule Event" };
            scheduleEvent.Click += (s, e) => { new ScheduleEvent().Show(); };
            actions.Add(scheduleEvent);

            //Send message
            Button sendMessage = new Button { Content = "Send message" };
            sendMessage.Click += (s, e) => { new SendMessage().Show(); };
            actions.Add(sendMessage);
            //Add Teacher Student Pair
            Button addTeacherStudentPair = new Button { Content = "Register student for teacher" };
            addTeacherStudentPair.Click += (s, e) => { new AddStudentTeacherPair().Show(); };
            actions.Add(addTeacherStudentPair);

            //Iterate over actions
            int row = 0;
            foreach (var action in actions)
            {
                action.Style = buttonStyle;
                action.Width = Double.NaN;
                action.Margin = new Thickness(10, 5, 10, 5);
                action.Click += (s, e) => { this.Visibility = Visibility.Hidden; };
                Grid.SetRow(action, row);
                row++;
                actionsGrid.RowDefinitions.Add(new RowDefinition());
                actionsGrid.Children.Add(action);
            }
        }
    }
}
