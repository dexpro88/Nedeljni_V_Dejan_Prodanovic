﻿using Nedeljni_IV_Dejan_Prodanovic.Command;
using Nedeljni_IV_Dejan_Prodanovic.Model;
using Nedeljni_IV_Dejan_Prodanovic.Service;
using Nedeljni_IV_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_IV_Dejan_Prodanovic.ViewModelBase
{
    class UserMainViewModel:ViewModelBase
    {
        UserMain view;
        IPostService postService;
        IUserService userService;
        List<tblUser> FriendList;
        List<int> CheckList;
        public UserMainViewModel(UserMain userView, tblUser userLogedIn)
        {
            view = userView;
            User = userLogedIn;
            postService = new PostService();
            userService = new UserService();
            PostList = postService.GetPosts();
            ListDto = ConvertListToDtoList(PostList);

            FriendList = userService.GetFriends(User);
            CheckList = FriendList.Select(item => item.UserID).ToList();
            SelectedPost = ListDto.FirstOrDefault();

          
           
                     
        }

        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private List<tblPost> postList;
        public List<tblPost> PostList
        {
            get
            {
                return postList;
            }
            set
            {
                postList = value;
                OnPropertyChanged("PostList");
            }
        }

        private List<PostDto> listDto;
        public List<PostDto> ListDto
        {
            get
            {
                return listDto;
            }
            set
            {
                listDto = value;
                OnPropertyChanged("ListDto");
            }
        }

        private PostDto selectedPost;
        public PostDto SelectedPost
        {
            get
            {
                return selectedPost;
            }
            set
            {
                selectedPost = value;
                OnPropertyChanged("SelectedPost");
            }
        }

        private ICommand addPost;
        public ICommand AddPost
        {
            get
            {
                if (addPost == null)
                {
                    addPost = new RelayCommand(param => AddPostExecute());
                }
                return addPost;
            }
        }

        private void AddPostExecute()
        {
            try
            {
                AddPost addPost = new AddPost(User);
                addPost.ShowDialog();
                PostList = postService.GetPosts();
                ListDto = ConvertListToDtoList(PostList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
      
        private ICommand showUsers;
        public ICommand ShowUsers
        {
            get
            {
                if (showUsers == null)
                {
                    showUsers = new RelayCommand(param => ShowUsersExecute(), 
                        param => CanShowUsersExecute());
                }
                return showUsers;
            }
        }

        private void ShowUsersExecute()
        {
            try
            {
                Users users = new Users(User);
                users.ShowDialog();
                 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowUsersExecute()
        {
            return true;
        }

        private ICommand showFriends;
        public ICommand ShowFriends
        {
            get
            {
                if (showFriends == null)
                {
                    showFriends = new RelayCommand(param => ShowFriendsExecute()
                        );
                }
                return showFriends;
            }
        }

        private void ShowFriendsExecute()
        {
            try
            {
                Friends friends = new Friends(User);
                friends.ShowDialog();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        

        private ICommand myProfile;
        public ICommand MyProfile
        {
            get
            {
                if (myProfile == null)
                {
                    myProfile = new RelayCommand(param => MyProfileExecute()
                        );
                }
                return myProfile;
            }
        }

        private void MyProfileExecute()
        {
            try
            {
                Profile profile = new Profile(User);
                profile.ShowDialog();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private ICommand showRequests;
        public ICommand ShowRequests
        {
            get
            {
                if (showRequests == null)
                {
                    showRequests = new RelayCommand(param => ShowRequestsExecute(),
                        param => CanShowRequestsExecute());
                }
                return showRequests;
            }
        }

        private void ShowRequestsExecute()
        {
            try
            {
                FriendRequests requests = new FriendRequests(User);
                requests.ShowDialog();
                FriendList = userService.GetFriends(User);
                CheckList = FriendList.Select(item => item.UserID).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowRequestsExecute()
        {
            return true;
        }
        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logout;
            }
        }

        private void LogoutExecute()
        {
            try
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                view.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanLogoutExecute()
        {
            return true;
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                view.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       

        private ICommand like;
        public ICommand Like
        {
            get
            {
                if (like == null)
                {
                    like = new RelayCommand(param => LikeExecute(), param => CanLikeExecute());
                }
                return like;
            }
        }

        private void LikeExecute()
        {
            try
            {
                tblPost postInDb = postService.GetPostById(selectedPost.PostID);
                MessageBox.Show("You liked this post");
                postService.LikePost(postInDb,User);

                PostList = postService.GetPosts();
                ListDto = ConvertListToDtoList(PostList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanLikeExecute()
        {
            List<int> usersToCheck = null;
            if (SelectedPost!=null)
            {
                var usersThatLiked = postService.UsersThatLikedPost(SelectedPost.PostID);
                usersToCheck = usersThatLiked.Select(item => item.UserID).ToList();
            }
           

            if (SelectedPost!=null)
            {
                if (CheckList.Contains((int)SelectedPost.UserID))
                {
                    if (!usersToCheck.Contains(User.UserID))
                    {
                        return true;
                    }
                   
                }
            }
            
            return false;
        }
        List<PostDto> ConvertListToDtoList(List<tblPost>postList)
        {
            List<PostDto> listDto = new List<PostDto>();

            foreach (var post in postList)
            {
                listDto.Add(ConvertToDto(post));
            }
            return listDto;
        }

        PostDto ConvertToDto(tblPost post)
        {
            PostDto postDto = new PostDto();
            postDto.PostID = post.PostID;
            postDto.DateOfPost = post.DateOfPost;
            postDto.PostText = post.PostText;
            postDto.NumberOfLikes = post.NumberOfLikes;
            postDto.UserID = post.UserID;

            if (post.UserID!=null)
            {
                postDto.User = userService.GetUserById((int)post.UserID);
            }
            

            return postDto;

        }
    }
}
 
